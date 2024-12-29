"use client";

import { Header } from "@/components/atoms/Header";
import { ProductSearchForm } from "../molecules/ProductSearchForm";
import { Surface } from "../atoms/Surface";
import { ProductDetail } from "../molecules/ProductDetail";
import { getProductByUrl } from "@/api/productApi";
import { useState } from "react";
import { Product } from "@/types/Product";

export const LandingPage = () => {
  const [product, setProduct] = useState<Product>();

  const handleSubmit = async (url: string) => {
    const data = await getProductByUrl(url);
    setProduct(data);
  };

  return (
    <>
      <Header />
      <main className="px-4 py-4 flex flex-col gap-4">
        <Surface>
          <ProductSearchForm onSubmit={handleSubmit} />
        </Surface>
        {product &&
          <Surface>
            <ProductDetail product={product} />
          </Surface>
        }
      </main>
    </>
  );
};
