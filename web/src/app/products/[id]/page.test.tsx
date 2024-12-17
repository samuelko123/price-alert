import { describe, expect, test } from 'vitest';
import { render, screen } from '@testing-library/react';
import Page from './page';

describe('Product Page', () => {
  test('displays product name', async () => {
    const component = await Page();

    render(component);

    const element = screen.getByRole("link", { name: "A dummy product" });
    expect(element).toBeVisible();
  })
});
