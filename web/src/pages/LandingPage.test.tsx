import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import Page from "@/app/page";

describe("Landing Page", () => {
  it("displays placeholder message", () => {
    render(<Page />);

    const element = screen.getByText("Features will be coming soon.");

    expect(element).toBeVisible();
  });
});
