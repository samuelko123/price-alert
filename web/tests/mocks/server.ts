import '@testing-library/jest-dom/vitest';
import { setupServer } from 'msw/node';
import { http, HttpResponse } from 'msw';

const handlers = [
  http.get('http://api:5000/api/products/1', () => {
    return HttpResponse.json({
      id: 1,
      url: "https://www.google.com",
      name: "A dummy product",
    })
  }),
];

export const server = setupServer(...handlers);
