import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";
import { Price } from "./Price";

describe("Price", () => {
  it.each`
    priceInCents | formattedPrice
    ${100}       | ${"$1.00"}
    ${120}       | ${"$1.20"}
    ${123}       | ${"$1.23"}
    ${123400}    | ${"$1,234.00"}
    ${123450}    | ${"$1,234.50"}
    ${123456}    | ${"$1,234.56"}
  `("displays $priceInCents as $formattedPrice", ({ priceInCents, formattedPrice }) => {
    // Act
    render(<Price priceInCents={priceInCents} />);

    // Assert
    expect(screen.getByText(formattedPrice)).toBeVisible();
  });
});
