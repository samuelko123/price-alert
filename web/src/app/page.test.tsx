import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import Page from './page';

describe('Home Page', () => {
  it('displays Hello World', () => {
    render(<Page />)

    const element = screen.getByText("Hello World");

    expect(element).toBeVisible();
  });
});
