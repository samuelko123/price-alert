import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductSearchForm } from "./ProductSearchForm";

describe("ProductSearchForm", () => {
  it("displays label", async () => {
    // Act
    render(<ProductSearchForm />);

    // Assert
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });

  it("displays text box", async () => {
    // Act
    render(<ProductSearchForm />);

    // Assert
    const input = screen.getByRole("textbox");
    expect(input).toBeVisible();
  });

  it("displays button", async () => {
    // Act
    render(<ProductSearchForm />);

    // Assert
    const button = screen.getByRole("button", { name: "Search" });
    expect(button).toBeVisible();
  });
});
