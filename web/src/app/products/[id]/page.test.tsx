import { expect, test } from 'vitest';
import { render, screen } from '@testing-library/react';
import Page from './page';

test('Page', () => {
  render(<Page />)

  const element = screen.getByRole("link", { name: "A dummy product" });

  expect(element).toBeVisible();
})
