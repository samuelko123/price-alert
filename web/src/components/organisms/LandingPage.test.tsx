import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import Page from "@/app/page";

describe("Landing Page", () => {
  it("displays Product Search Form", () => {
    // Act
    render(<Page />);

    // Assert
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });

  it("displays Product Detail", () => {
    // Act
    render(<Page />);

    // Assert
    const product = screen.getByText("The Best Product");
    expect(product).toBeVisible();
  });
});
