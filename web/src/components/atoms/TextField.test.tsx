import { describe, expect, it, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { TextField } from "./TextField";
import userEvent from "@testing-library/user-event";

describe("TextField", () => {
  it("focuses on the text box when the label is clicked", async () => {
    // Arrange
    const user = userEvent.setup();

    const labelText = "It is a label";
    render(<TextField
      label={labelText}
      value=""
      onChange={() => { }}
    />);

    // Act
    const label = screen.getByText(labelText);
    await user.click(label);

    // Assert
    const textbox = screen.getByRole("textbox");
    expect(textbox).toHaveFocus();
  });

  it("passes value to the textbox", async () => {
    // Arrange
    render(<TextField
      label=""
      value="hello"
      onChange={() => { }}
    />);

    // Act
    const textbox = screen.getByRole("textbox");

    // Assert
    expect(textbox).toHaveAttribute("value", "hello");
  });

  it("triggers onChange when user inputs", async () => {
    // Arrange
    const user = userEvent.setup();

    const mockFunction = vi.fn();
    render(<TextField
      label=""
      value=""
      onChange={mockFunction}
    />);

    // Act
    const textbox = screen.getByRole("textbox");
    await user.type(textbox, "Hello");

    // Assert
    expect(mockFunction).toHaveBeenCalled();
  });
});
