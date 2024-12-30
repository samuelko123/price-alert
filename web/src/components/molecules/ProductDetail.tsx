import { Product } from "@/types/Product";

export const ProductDetail = ({
  product,
}: {
  product: Product,
}) => {
  return (
    <div>
      <p>{product.sku}</p>
      <p>{product.name}</p>
    </div>
  );
};
