import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ErrorMessage } from "./ErrorMessage";

describe("ErrorMessage", () => {
  it("displays generic error message", async () => {
    // Act
    render(<ErrorMessage error={new Error()} />);

    // Assert
    const message = screen.getByText("Something went wrong.");
    expect(message).toBeVisible();
  });
});
