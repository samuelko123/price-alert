"use client";

import { getProductByUrl } from "@/api/productApi";
import { useMutation } from "@tanstack/react-query";

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
