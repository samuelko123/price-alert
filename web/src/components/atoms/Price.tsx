export const Price = ({
  priceInCents,
}: {
  priceInCents: number,
}) => {
  const formatter = new Intl.NumberFormat("en-AU", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  const price = formatter.format(
    priceInCents / 100,
  );

  return (
    <p>{`$${price}`}</p>
  );
};
