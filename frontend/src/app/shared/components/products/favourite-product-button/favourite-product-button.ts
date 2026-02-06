import {ChangeDetectionStrategy, Component, computed, DestroyRef, inject, input, signal} from '@angular/core';
import {Button} from 'primeng/button';
import {FavouritesService} from '../../../../core/services/favourites.service';
import {takeUntilDestroyed, toSignal} from '@angular/core/rxjs-interop';
import {IdentityService} from '../../../../core/services/identity.service';
import {User} from '../../../../core/api/models/user';
import {Product} from '../../../../core/api/models/product';
import {finalize, iif} from 'rxjs';

@Component({
  selector: 'app-favourite-product-button',
  imports: [
    Button
  ],
  templateUrl: './favourite-product-button.html',
  styleUrl: './favourite-product-button.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FavouriteProductButton {
  product = input.required<Product>();

  private _favouritesService = inject(FavouritesService);
  private _identityService = inject(IdentityService);
  private _destroyRef = inject(DestroyRef);

  private _user = toSignal<User | null | undefined>(this._identityService.user$, {initialValue: null});
  private _favourites = toSignal<Product[]>(this._favouritesService.favourites$);
  private _isLoading = signal(false);

  favouriteProductId = computed<string | null>(() => this._favourites()?.find(x => x.externalId === this.product().externalId)?.id ?? null)
  isChecked = computed<boolean>(() => (this.product().id !== null || this.favouriteProductId() !== null) ?? false);
  isDisabled = computed<boolean>(() => this._user() === null || this._isLoading());

  handleFavouriteClick(): void {
    this._isLoading.set(true)
    iif(
      () => !this.isChecked(),
      this._favouritesService.addFavourite(this.product()),
      iif(
        () => this.product().id !== null,
        this._favouritesService.deleteFavourite(this.product().id!),
        this._favouritesService.deleteFavourite(this.favouriteProductId()!)
      )
    ).pipe(
      takeUntilDestroyed(this._destroyRef),
      finalize(() => this._isLoading.set(false))
    ).subscribe();
  }
}
