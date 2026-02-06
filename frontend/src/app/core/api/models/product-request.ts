export interface ProductRequest {
  externalId: string;
  externalUrl: string;
  photoUrl: string;
  title: string;
  price: string | null;
  rating: string | null;
}
