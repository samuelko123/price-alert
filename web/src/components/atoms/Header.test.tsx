import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Header } from "./Header";

describe("Header", () => {
  it("displays text", () => {
    // Act
    render(<Header />);

    // Assert
    const text = screen.getByText("Price Alert");
    expect(text).toBeVisible();
  });

  it("displays logo", async () => {
    // Act
    render(<Header />);

    // Assert
    const image = screen.getByAltText("App Logo");
    expect(image).toBeVisible();
  });

  it("displays link", async () => {
    // Act
    render(<Header />);

    // Assert
    const link = screen.getByRole("link");
    expect(link).toHaveAttribute("href", "/");
  });
});
