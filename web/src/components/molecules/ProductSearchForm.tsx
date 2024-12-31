"use client";

import { useState } from "react";

import { useProduct } from "@/hooks/useProduct";

import { Alert } from "../atoms/Alert";
import { Button } from "../atoms/Button";
import { ErrorMessage } from "../atoms/ErrorMessage";
import { LoadingMessage } from "../atoms/LoadingMessage";
import { Surface } from "../atoms/Surface";
import { TextField } from "../atoms/TextField";
import { ProductDetail } from "./ProductDetail";

export const ProductSearchForm = () => {
  const [url, setUrl] = useState("");
  const { isError, isPending, data: product, error, mutate } = useProduct({ url });

  return (
    <Surface>
      <div className="flex flex-col gap-4">
        <form
          className="flex flex-col gap-4 items-start"
          onSubmit={(event) => {
            event.preventDefault();
            mutate();
          }}
        >
          <TextField
            label="Product URL"
            value={url}
            onChange={setUrl}
          />
          {isPending ?
            <LoadingMessage />
            :
            <Button type="submit">
              Search
            </Button>
          }
        </form>
        {
          isError &&
          <Alert>
            <ErrorMessage error={error} />
          </Alert>
        }
        {
          product &&
          <ProductDetail product={product} />
        }
      </div>
    </Surface>
  );
};
