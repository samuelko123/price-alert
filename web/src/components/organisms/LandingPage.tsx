import { Header } from "@/components/atoms/Header";
import { ProductSearchForm } from "../molecules/ProductSearchForm";
import { Surface } from "../atoms/Surface";

export const LandingPage = () => {
  return (
    <>
      <Header />
      <main className="px-4 py-4">
        <Surface>
          <ProductSearchForm />
        </Surface>
      </main>
    </>
  );
};
