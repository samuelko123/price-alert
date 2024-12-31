import { Product } from "@/types/Product";

export const ProductDetail = ({
  product,
}: {
  product: Product,
}) => {
  const formatter = new Intl.NumberFormat("en-AU", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  const price = formatter.format(
    product.priceInCents / 100,
  );

  return (
    <div>
      <p>{product.sku}</p>
      <p>{product.name}</p>
      <p>${price}</p>
    </div>
  );
};
