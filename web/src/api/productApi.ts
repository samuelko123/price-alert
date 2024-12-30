import { Problem } from "@/errors/Problem";
import { ValidationProblem } from "@/errors/ValidationProblem";
import { Product } from "@/types/Product";

export const getProductByUrl = async (url: string): Promise<Product> => {
  const res = await fetch(`/api/products/getByUrl?url=${url}`);

  const data = await res.json();
  if (res.headers.get("Content-Type")?.includes("application/problem+json")) {
    if (res.status === 400) {
      throw new ValidationProblem(data);
    }

    throw new Problem(data);
  }

  return data;
};
