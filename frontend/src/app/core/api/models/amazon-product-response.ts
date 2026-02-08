export interface AmazonProductResponse {
  status: string;
  request_id: string;
  parameters: object;
  data: {
    best_sellers: ProductAmazon[]
    category_name: string;
    country: string;
    domain: string;
  }
}

export interface ProductAmazon {
  rank: number,
  asin: string,
  product_title: string,
  product_price: string | null,
  product_star_rating: string | null,
  product_num_ratings: number | null,
  product_url: string,
  product_photo: string,
  rank_change_label: string | null
}
