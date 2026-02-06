import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavouriteProductButton } from './favourite-product-button';

describe('FavouriteButton', () => {
  let component: FavouriteProductButton;
  let fixture: ComponentFixture<FavouriteProductButton>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FavouriteProductButton]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FavouriteProductButton);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
