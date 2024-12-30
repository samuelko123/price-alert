"use client";

import { Header } from "@/components/atoms/Header";
import { ProductSearchForm } from "../molecules/ProductSearchForm";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

export const LandingPage = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Header />
      <main className="max-w-sm m-auto px-4 py-4">
        <ProductSearchForm />
      </main>
    </QueryClientProvider>
  );
};
