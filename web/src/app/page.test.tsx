import { render, screen } from '@testing-library/react';
import Page from './page';

describe('Page', () => {
  it('displays Hello World', () => {
    render(<Page />);

    const element = screen.getByText("Hello World - It will fail");

    expect(element).toBeVisible();
  })
})
