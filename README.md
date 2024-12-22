<h1>Price Alert (WIP)</h1>

<p>A hobbyist project to send email notification on price drop on subscribed items. </p>
<p>You can visit https://price-alert.samuelko123.dev/.</p>

<h2>How to Run Locally</h2>

```
docker compose --file ./infra/docker/docker-compose.yaml up --build --remove-orphans
```

<p>Then, go to http://localhost:4000/</p>

<h2>How to Run Unit Test</h2>

```
docker compose --file ./infra/docker/docker-compose.test.yaml up --build --remove-orphans
```

<h2>Further Readings</h2>
<ul>
  <li>
    <a href="./infra/README.md">Infrastructure</a>
  </li>
<ul>
