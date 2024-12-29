import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Button } from "./Button";

describe("Button", () => {
  it("displays text", async () => {
    // Arrange
    const buttonText = "It is a button";

    // Act
    render(<Button>{buttonText}</Button>);

    // Assert
    const button = screen.getByText(buttonText);
    expect(button).toBeVisible();
  });
});
