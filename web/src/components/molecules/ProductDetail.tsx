import { Product } from "@/types/Product";
import { Price } from "../atoms/Price";

export const ProductDetail = ({
  product,
}: {
  product: Product,
}) => {
  return (
    <div>
      <p>{product.sku}</p>
      <p>{product.name}</p>
      <Price priceInCents={product.priceInCents} />
    </div>
  );
};
