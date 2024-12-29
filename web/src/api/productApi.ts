import { Product } from "@/types/Product";

export const getProductByUrl = async (url: string): Promise<Product> => {
  const res = await fetch(`/api/products/getByUrl?url=${url}`);
  return await res.json();
};
