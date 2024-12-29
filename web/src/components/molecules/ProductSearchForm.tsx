"use client";

import { useState } from "react";
import { Button } from "../atoms/Button";
import { TextField } from "../atoms/TextField";

export const ProductSearchForm = () => {
  const [url, setUrl] = useState("");

  return (
    <form
      className="flex flex-col gap-2 items-start"
      onSubmit={(event) => event.preventDefault()}
    >
      <TextField
        label="Product URL"
        value={url}
        onChange={setUrl}
      />
      <Button>Search</Button>
    </form >
  );
};
