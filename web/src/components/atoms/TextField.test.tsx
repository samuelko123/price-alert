import { beforeAll, describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { TextField } from "./TextField";
import userEvent from "@testing-library/user-event";

describe("TextField", () => {
  const labelText = "It is a label";

  beforeAll(() => {
    render(<TextField label={labelText} />);
  });

  it("focuses on the text box when the label is clicked", async () => {
    const user = userEvent.setup();

    const label = screen.getByText(labelText);
    await user.click(label);

    const input = screen.getByRole("textbox");
    expect(input).toHaveFocus();
  });
});
