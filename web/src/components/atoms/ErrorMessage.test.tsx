import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ErrorMessage } from "./ErrorMessage";

describe("ErrorMessage", () => {
  it("displays text", async () => {
    // Act
    render(<ErrorMessage />);

    // Assert
    const message = screen.getByText("Something went wrong.");
    expect(message).toBeVisible();
  });
});
