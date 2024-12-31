"use client";

import { useMutation } from "@tanstack/react-query";

import { getProductByUrl } from "@/api/productApi";

export const useProduct = ({
  url,
}: {
  url: string,
}) => {
  return useMutation({
    mutationFn: async () => {
      return await getProductByUrl(url);
    },
  });
};
