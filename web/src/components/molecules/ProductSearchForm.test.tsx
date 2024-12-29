import { describe, expect, it, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductSearchForm } from "./ProductSearchForm";
import userEvent from "@testing-library/user-event";

describe("ProductSearchForm", () => {
  it("displays label", async () => {
    // Act
    render(<ProductSearchForm onSubmit={() => { }} />);

    // Assert
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });

  it("displays text box", async () => {
    // Act
    render(<ProductSearchForm onSubmit={() => { }} />);

    // Assert
    const input = screen.getByRole("textbox");
    expect(input).toBeVisible();
  });

  it("displays button", async () => {
    // Act
    render(<ProductSearchForm onSubmit={() => { }} />);

    // Assert
    const button = screen.getByRole("button", { name: "Search" });
    expect(button).toBeVisible();
  });

  it("triggers onSubmit when user clicks the button", async () => {
    // Arrange
    const user = userEvent.setup();

    const mockFunction = vi.fn();
    render(<ProductSearchForm onSubmit={mockFunction} />);

    // Act
    const textbox = screen.getByRole("textbox");
    await user.type(textbox, "https://google.com");
    const button = screen.getByRole("button");
    await user.click(button);

    // Assert
    expect(mockFunction).toHaveBeenCalledWith("https://google.com");
  });
});
