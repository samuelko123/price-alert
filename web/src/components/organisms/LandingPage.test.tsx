import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import Page from "@/app/page";

describe("Landing Page", () => {
  it("displays Product Search Form", () => {
    // Act
    render(<Page />);

    // Assert
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });
});
