import { describe, expect, it } from "vitest";
import { getProductByUrl } from "./productApi";
import { http, HttpResponse, server } from "../../tests/setup-server";

describe("getProductByUrl", () => {
  it("returns a product", async () => {
    // Arrange
    server.use(
      http.get("/api/products/getByUrl", () => {
        return HttpResponse.json({
          sku: "123",
          name: "A dummy product",
        });
      }),
    );

    // Act
    const product = await getProductByUrl("https://google.com");

    // Assert
    expect(product.sku).toBe("123");
    expect(product.name).toBe("A dummy product");
  });
});
