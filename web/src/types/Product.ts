export type Product = {
  sku: string,
  name: string,
  priceInCents: number,
  mainImage: Image,
};

type Image = {
  src: string,
};
