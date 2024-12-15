import { render, screen } from '@testing-library/react';
import Page from './page';

describe('Page', () => {
  it('renders a heading', () => {
    render(<Page />);

    const element = screen.getByText("Hello World");

    expect(element).toBeVisible();
  })
})