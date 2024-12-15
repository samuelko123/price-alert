import { render, screen } from '@testing-library/react';
import Page from './page';

describe('Page', () => {
  it('displays Hello World', () => {
    render(<Page />);

    const element = screen.getByText("Hello Worl");

    expect(element).toBeVisible();
  })
})
