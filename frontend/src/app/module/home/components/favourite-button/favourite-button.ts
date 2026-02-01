import {ChangeDetectionStrategy, Component, computed, inject, input} from '@angular/core';
import {Button} from 'primeng/button';
import {FavouritesService} from '../../../../core/services/favourites.service';
import {toSignal} from '@angular/core/rxjs-interop';
import {IdentityService} from '../../../../core/services/identity.service';
import {User} from '../../../../core/api/models/user';

@Component({
  selector: 'app-favourite-button',
  imports: [
    Button
  ],
  templateUrl: './favourite-button.html',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FavouriteButton {
  productId = input.required<number>();

  private _favouritesService = inject(FavouritesService);
  private _identityService = inject(IdentityService);

  private _user = toSignal<User | null>(this._identityService.user$, {initialValue: null});
  private _favourites = toSignal<Set<string>>(this._favouritesService.favourites$);

  isChecked = computed<boolean>(() => this._favourites()?.has(this.productId().toString()) ?? false);
  isDisabled = computed<boolean>(() => this._user() === null);

  handleFavouriteClick(): void {
    if (!this.isChecked()) {
      this._favouritesService.addFavourite(this.productId())
        .subscribe()
    } else {
      this._favouritesService.deleteFavourite(this.productId())
        .subscribe()
    }
  }
}
