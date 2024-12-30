import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { ProductSearchForm } from "./ProductSearchForm";
import userEvent from "@testing-library/user-event";
import { http, HttpResponse, server } from "../../../tests/setup-server";

describe("ProductSearchForm", () => {
  it("displays Product Detail after search", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", ({ request }) => {
        const url = new URL(request.url);
        const productUrl = url.searchParams.get("url");
        if (productUrl != "https://google.com") {
          return new HttpResponse(null, { status: 404 });
        }

        return HttpResponse.json({
          sku: "123",
          name: "A dummy product",
        });
      }),
    );

    const user = userEvent.setup();
    render(<ProductSearchForm />);

    // Act
    const textbox = screen.getByRole("textbox", { name: "Product URL" });
    await user.type(textbox, "https://google.com");
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const product = screen.getByText("A dummy product");
    expect(product).toBeVisible();
  });

  it("displays error when response is not json", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        return new HttpResponse("It is not json.");
      }),
    );

    const user = userEvent.setup();
    render(<ProductSearchForm />);

    // Act
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const error = screen.getByText("Something went wrong.");
    expect(error).toBeVisible();
  });
});
