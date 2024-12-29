import { Header } from "@/components/atoms/Header";
import { ProductSearchForm } from "../molecules/ProductSearchForm";
import { Surface } from "../atoms/Surface";
import { ProductDetail } from "../molecules/ProductDetail";

export const LandingPage = () => {
  const product = {
    sku: "123",
    name: "The Best Product",
  };

  return (
    <>
      <Header />
      <main className="px-4 py-4 flex flex-col gap-4">
        <Surface>
          <ProductSearchForm />
        </Surface>
        <Surface>
          <ProductDetail product={product} />
        </Surface>
      </main>
    </>
  );
};
