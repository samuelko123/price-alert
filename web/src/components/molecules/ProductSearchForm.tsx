import { Button } from "../atoms/Button";
import { TextField } from "../atoms/TextField";

export const ProductSearchForm = () => {
  return (
    <form className="flex flex-col gap-2 items-start">
      <TextField
        label="Product URL"
      />
      <Button>Search</Button>
    </form>
  );
};
