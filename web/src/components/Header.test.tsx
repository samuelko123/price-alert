import { beforeAll, describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Header } from "./Header";

describe("Header", () => {
  beforeAll(() => {
    render(<Header />);
  });

  it("displays text", () => {
    const text = screen.getByText("Price Alert");
    expect(text).toBeVisible();
  });

  it("displays logo", async () => {
    const image = screen.getByAltText("App Logo");
    expect(image).toBeVisible();
  });

  it("displays link", async () => {
    const link = screen.getByRole("link");
    expect(link).toHaveAttribute("href", "/");
  });
});
