export interface Product {
  id?: string | null,
  externalId: string;
  externalUrl: string;
  photoUrl: string;
  title: string;
  price: string | null;
  rating: string | null;
}
