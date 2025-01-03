import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { beforeEach, describe, expect, it } from "vitest";

import { delay, http, HttpResponse, server } from "../../../tests/setup-server";
import { ProductSearchForm } from "./ProductSearchForm";

const queryClient = new QueryClient();

describe("ProductSearchForm", () => {
  beforeEach(() => {
    render(
      <QueryClientProvider client={queryClient}>
        <ProductSearchForm />
      </QueryClientProvider>,
    );
  });

  it("displays product information from API response", async () => {
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
          name: "a product",
          priceInCents: 123456,
          mainImage: {
            src: "https://google.com/an-image.png",
          },
        });
      }),
    );

    const user = userEvent.setup();

    // Act
    const textbox = screen.getByRole("textbox", { name: "Product URL" });
    await user.type(textbox, "https://google.com");
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    expect(screen.getByText("123")).toBeVisible();
    expect(screen.getByText("a product")).toBeVisible();
    expect(screen.getByText("$1,234.56")).toBeVisible();
    expect(screen.getByAltText("product image")).toBeVisible();
  });

  it("displays generic error message when response is not json", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        return new HttpResponse("It is not json.");
      }),
    );

    const user = userEvent.setup();

    // Act
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const error = screen.getByText("Something went wrong.");
    expect(error).toBeVisible();
  });

  it("displays error message from API response if content-type is problem", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        const data = {
          "type": "https://httpstatuses.io/500",
          "title": "An error occurred while processing your request.",
          "status": 500,
        };

        return HttpResponse.json(data, {
          status: 500,
          headers: {
            "Content-Type": "application/problem+json",
          },
        });
      }),
    );

    const user = userEvent.setup();

    // Act
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const product = screen.getByText("An error occurred while processing your request.");
    expect(product).toBeVisible();
  });

  it("displays validation error message from server status code is 400", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        const data = {
          "type": "https://httpstatuses.io/400",
          "title": "One or more validation errors occurred.",
          "status": 400,
          "errors": [
            {
              "message": "The url is invalid.",
            },
          ],
        };

        return HttpResponse.json(data, {
          status: 400,
          headers: {
            "Content-Type": "application/problem+json",
          },
        });
      }),
    );

    const user = userEvent.setup();

    // Act
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const product = screen.getByText("The url is invalid.");
    expect(product).toBeVisible();
  });

  it("displays loading message during API call", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", async () => {
        await delay(5000);
      }),
    );

    const user = userEvent.setup();

    // Act
    const button = screen.getByRole("button", { name: "Search" });
    await user.click(button);

    // Assert
    const product = screen.getByText("We are fetching your item...");
    expect(product).toBeVisible();
  });
});
