import { Problem } from "@/errors/Problem";
import { Product } from "@/types/Product";

export const getProductByUrl = async (url: string): Promise<Product> => {
  const res = await fetch(`/api/products/getByUrl?url=${url}`);
  const data = await res.json();
  if (res.headers.get("Content-Type") === "application/problem+json") {
    throw new Problem(data.title);
  }
  return data;
};
