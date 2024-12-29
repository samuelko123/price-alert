"use client";

import { useState } from "react";
import { Button } from "../atoms/Button";
import { TextField } from "../atoms/TextField";
import { getProductByUrl } from "@/api/productApi";
import { Product } from "@/types/Product";
import { Surface } from "../atoms/Surface";
import { ProductDetail } from "./ProductDetail";

export const ProductSearchForm = () => {
  const [url, setUrl] = useState("");
  const [product, setProduct] = useState<Product>();

  const handleSubmit = async () => {
    const data = await getProductByUrl(url);
    setProduct(data);
  };

  return (
    <>
      <Surface>
        <form
          className="flex flex-col gap-2 items-start"
          onSubmit={(event) => event.preventDefault()}
        >
          <TextField
            label="Product URL"
            value={url}
            onChange={setUrl}
          />
          <Button onClick={() => handleSubmit()}>Search</Button>
        </form>
      </Surface>
      {product &&
        <Surface>
          <ProductDetail product={product} />
        </Surface>
      }
    </>
  );
};
