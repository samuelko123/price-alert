"use client";

import { useState } from "react";
import { Button } from "../atoms/Button";
import { TextField } from "../atoms/TextField";

export const ProductSearchForm = ({
  onSubmit,
}: {
  onSubmit: (url: string) => void,
}) => {
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
      <Button onClick={() => onSubmit(url)}>Search</Button>
    </form>
  );
};
