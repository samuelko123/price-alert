import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import Page from "@/app/page";
import userEvent from "@testing-library/user-event";
import { http, HttpResponse, server } from "../../../tests/setup-server";

describe("Landing Page", () => {
  it("displays Product Search Form", () => {
    // Act
    render(<Page />);

    // Assert
    const label = screen.getByText("Product URL");
    expect(label).toBeVisible();
  });

  it("displays Product Detail after search", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        return HttpResponse.json({
          sku: "123",
          name: "A dummy product",
        });
      }),
    );

    const user = userEvent.setup();
    render(<Page />);

    // Act
    const textbox = screen.getByRole("textbox", { name: "Product URL" });
    await user.type(textbox, "https://google.com");
    const button = screen.getByRole("button");
    await user.click(button);

    // Assert
    const product = screen.getByText("A dummy product");
    expect(product).toBeVisible();
  });
});
