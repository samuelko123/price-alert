import { beforeAll, describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Surface } from "./Surface";

describe("Surface", () => {
  beforeAll(() => {
    render(
      <Surface>
        <button>Hello World</button>
      </Surface>,
    );
  });

  it("displays its children", async () => {
    const button = screen.getByText("Hello World");
    expect(button).toBeVisible();
  });
});
