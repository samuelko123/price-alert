"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import { Header } from "@/components/atoms/Header";

import { Surface } from "../atoms/Surface";
import { ProductSearchForm } from "../molecules/ProductSearchForm";

const queryClient = new QueryClient();

export const LandingPage = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Header />
      <main className="max-w-sm m-auto px-4 py-4 flex flex-col gap-4">
        <Surface>
          <p>Supported merchants:</p>
          <p>- Officeworks</p>
        </Surface>
        <ProductSearchForm />
      </main>
    </QueryClientProvider>
  );
};
