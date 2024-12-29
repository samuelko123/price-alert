import { describe, expect, it, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { Button } from "./Button";
import userEvent from "@testing-library/user-event";

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

  it("triggers onClick when user clicks", async () => {
    // Arrange
    const user = userEvent.setup();

    const mockFunction = vi.fn();
    render(<Button onClick={mockFunction}>Hello</Button>);

    // Act
    const button = screen.getByRole("button");
    await user.click(button);

    // Assert
    expect(mockFunction).toHaveBeenCalledOnce();
  });
});
