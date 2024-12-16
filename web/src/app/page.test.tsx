import '@testing-library/jest-dom/vitest';
import { expect, test } from 'vitest';
import { render, screen } from '@testing-library/react';
import Page from './page';

test('Page', () => {
  render(<Page />)

  const element = screen.getByText("Hello World");

  expect(element).toBeVisible();
})
