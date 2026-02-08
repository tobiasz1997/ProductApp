import {ChangeDetectionStrategy, Component, computed, inject, input} from '@angular/core';
import {Button} from 'primeng/button';
import {IdentityService} from '../../../../core/services/identity.service';
import {Product} from '../../../../core/api/models/product';
import {FavouritesStore} from '../../../../core/store/favourites.store';
import {UserStore} from '../../../../core/store/user.store';

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

  private readonly _favouritesStore = inject(FavouritesStore);
  private readonly _userStore = inject(UserStore)
  private readonly _identityService = inject(IdentityService);

  favouriteProductId = computed<string | null>(() => this._favouritesStore.favourites()?.find(x => x.externalId === this.product().externalId)?.id ?? null)
  isChecked = computed<boolean>(() => (this.product().id !== null || this.favouriteProductId() !== null) ?? false);
  isDisabled = computed<boolean>(() => this._userStore.user() === null || this._favouritesStore.isActionLoading());

  handleFavouriteClick(): void {
    if (!this.isChecked()) {
      this._favouritesStore.addFavourite(this.product())
    } else {
      if (this.product().id !== null) {
        this._favouritesStore.deleteFavourite(this.product().id!)
      } else {
        this._favouritesStore.deleteFavourite(this.favouriteProductId()!)
      }
    }
  }
}
