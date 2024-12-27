import { beforeAll, describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductSearchForm } from "./ProductSearchForm";

describe("ProductSearchForm", () => {
  beforeAll(() => {
    render(<ProductSearchForm />);
  });

  it("displays label", async () => {
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });

  it("displays text box", async () => {
    const input = screen.getByRole("textbox");
    expect(input).toBeVisible();
  });

  it("displays button", async () => {
    const button = screen.getByRole("button", { name: "Search" });
    expect(button).toBeVisible();
  });
});
