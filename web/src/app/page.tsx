// this line can be removed once the page becomes dynamic, i.e. with url path parameter
export const dynamic = 'force-dynamic'

export default async function Home() {
  const res = await fetch('http://reverse-proxy:4000/api/products/1');
  const product = await res.json();

  return (
    <main>
      <h1>Hello World</h1>
      <p>
        <a href={product.url}>{product.name}</a>
      </p>
    </main>
  );
}
