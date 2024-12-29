import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Surface } from "./Surface";

describe("Surface", () => {
  it("displays its children", async () => {
    // Act
    render(
      <Surface>
        <button>Hello World</button>
      </Surface>,
    );

    // Assert
    const button = screen.getByText("Hello World");
    expect(button).toBeVisible();
  });
});
