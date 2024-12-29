import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { TextField } from "./TextField";
import userEvent from "@testing-library/user-event";

describe("TextField", () => {
  it("focuses on the text box when the label is clicked", async () => {
    // Arrange
    const user = userEvent.setup();

    const labelText = "It is a label";
    render(<TextField label={labelText} />);

    // Act
    const label = screen.getByText(labelText);
    await user.click(label);

    // Assert
    const input = screen.getByRole("textbox");
    expect(input).toHaveFocus();
  });
});
