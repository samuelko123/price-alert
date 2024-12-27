import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import Page from "@/app/page";

describe("Landing Page", () => {
  it("displays Product Search Form", () => {
    render(<Page />);

    const element = screen.getByText("Product URL");

    expect(element).toBeVisible();
  });
});
