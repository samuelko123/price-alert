export default async function Page() {
  const res = await fetch("http://api:5000/api/products/1");
  const product = await res.json();

  return (
    <main>
      <p>
        <a href={product.url}>{product.name}</a>
      </p>
    </main>
  );
}
