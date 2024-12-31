import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductDetail } from "./ProductDetail";

describe("ProductDetail", () => {
  it("displays sku, name and price", async () => {
    // Arrange
    const product = {
      sku: "123",
      name: "a product",
      priceInCents: 123456,
      mainImage: {
        src: "https://google.com/an-image.png",
      },
    };

    // Act
    render(<ProductDetail product={product} />);

    // Assert
    expect(screen.getByText("123")).toBeVisible();
    expect(screen.getByText("a product")).toBeVisible();
    expect(screen.getByText("$1,234.56")).toBeVisible();
    expect(screen.getByAltText("product image")).toBeVisible();
  });
});
