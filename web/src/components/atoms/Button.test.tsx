import { beforeAll, describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Button } from "./Button";

describe("Button", () => {
  const buttonText = "It is a button";

  beforeAll(() => {
    render(<Button>{buttonText}</Button>);
  });

  it("displays text", async () => {
    const button = screen.getByText(buttonText);
    expect(button).toBeVisible();
  });
});
