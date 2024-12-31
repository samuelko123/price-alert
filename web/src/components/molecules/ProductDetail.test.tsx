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
    };

    // Act
    render(<ProductDetail product={product} />);

    // Assert
    expect(screen.getByText(product.sku)).toBeVisible();
    expect(screen.getByText(product.name)).toBeVisible();
    expect(screen.getByText("$1,234.56")).toBeVisible();
  });
});
