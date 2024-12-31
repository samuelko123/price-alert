<h1>Price Alert (WIP)</h1>

<p>A hobbyist project to send email notification on price drop on subscribed items. </p>
<p>It is hosted on https://price-alert.samuelko123.dev/.</p>

<h2>Available Commands</h2>

| Command               | Description                                                                             |
| ----------------------| --------------------------------------------------------------------------------------- |
| `./run.ps1 prod`      | Start Docker containers in production mode.                                             |
| `./run.ps1 dev`       | Start Docker containers in development mode, with Hot Module Replacement enabled.       |
| `./run.ps1 stop`      | Stop Docker containers.                                                                 |
| `./run.ps1 uninstall` | Remove all Docker resources related to this project.                                    |
| `./run.ps1 test`      | Run unit tests.                                                                         |

Both `dev` and `prod` are hosted on http://localhost:4000.

<h2>More Documentations</h2>
<ul>
  <li>
    <a href="./infra/README.md">DevOps Infrastructure</a>
  </li>
<ul>
