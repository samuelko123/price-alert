"use client";

import { Header } from "@/components/atoms/Header";
import { ProductSearchForm } from "../molecules/ProductSearchForm";

export const LandingPage = () => {
  return (
    <>
      <Header />
      <main className="max-w-sm m-auto px-4 py-4">
        <ProductSearchForm />
      </main>
    </>
  );
};
