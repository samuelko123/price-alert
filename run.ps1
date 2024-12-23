#!pwsh
param(
  [Parameter(Position = 0, Mandatory = $true)]
  [ValidateSet("dev", "prod", "test")]
  [string]$profile
)

function RunCommand([string]$command) {
  Write-Host "Running the following command:"
  Write-Host $command
  Invoke-Expression $command
}

switch ($profile) {
  "dev" {
    RunCommand("docker compose --file ./infra/docker/docker-compose.yaml --file ./infra/docker/docker-compose.dev.yaml down --remove-orphans")
    RunCommand("docker compose --file ./infra/docker/docker-compose.yaml --file ./infra/docker/docker-compose.dev.yaml up --build --detach")
  }
  "prod" {
    RunCommand("docker compose --file ./infra/docker/docker-compose.yaml down --remove-orphans")
    RunCommand("docker compose --file ./infra/docker/docker-compose.yaml up --build --detach")
  }
  "test" {
    RunCommand("docker compose --file ./infra/docker/docker-compose.test.yaml down --rmi all --remove-orphans")
    RunCommand("docker compose --file ./infra/docker/docker-compose.test.yaml up --build")
  }
}
