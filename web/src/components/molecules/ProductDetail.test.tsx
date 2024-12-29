import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductDetail } from "./ProductDetail";

describe("ProductDetail", () => {
  it("displays sku and name", async () => {
    // Arrange
    const product = {
      sku: "123",
      name: "a product",
    };

    // Act
    render(<ProductDetail product={product} />);

    // Assert
    expect(screen.getByText(product.sku)).toBeVisible();
    expect(screen.getByText(product.name)).toBeVisible();
  });
});
