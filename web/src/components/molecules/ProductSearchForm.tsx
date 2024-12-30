"use client";

import { useState } from "react";
import { Button } from "../atoms/Button";
import { TextField } from "../atoms/TextField";
import { getProductByUrl } from "@/api/productApi";
import { Product } from "@/types/Product";
import { Surface } from "../atoms/Surface";
import { ProductDetail } from "./ProductDetail";
import { ErrorMessage } from "../atoms/ErrorMessage";
import { LoadingMessage } from "../atoms/LoadingMessage";

export const ProductSearchForm = () => {
  const [url, setUrl] = useState("");
  const [product, setProduct] = useState<Product | null>();
  const [error, setError] = useState<Error | null>();
  const [isLoading, setLoading] = useState(false);

  const handleSubmit = async () => {
    try {
      setLoading(true);
      setError(null);
      setProduct(null);

      const data = await getProductByUrl(url);
      setProduct(data);
    } catch (err) {
      setError(err as Error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex flex-col gap-4">
      <Surface>
        <form
          className="flex flex-col gap-4 items-start"
          onSubmit={(event) => event.preventDefault()}
        >
          <TextField
            label="Product URL"
            value={url}
            onChange={setUrl}
          />
          {isLoading ?
            <LoadingMessage />
            :
            <Button
              onClick={() => handleSubmit()}
            >
              Search
            </Button>
          }
        </form>
      </Surface>
      {error &&
        <Surface>
          <ErrorMessage error={error} />
        </Surface>
      }
      {product &&
        <Surface>
          <ProductDetail product={product} />
        </Surface>
      }
    </div>
  );
};
