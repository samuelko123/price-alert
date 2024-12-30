import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { LoadingMessage } from "./LoadingMessage";

describe("LoadingMessage", () => {
  it("displays text", async () => {
    // Act
    render(<LoadingMessage />);

    // Assert
    const message = screen.getByText("We are fetching your item...");
    expect(message).toBeVisible();
  });
});
