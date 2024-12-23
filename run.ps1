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
    $compose = "docker compose --file ./infra/docker/docker-compose.yaml --file ./infra/docker/docker-compose.dev.yaml"
    RunCommand("$compose down --rmi all --remove-orphans")
    RunCommand("$compose up --build --detach")
  }
  "prod" {
    $compose = "docker compose --file ./infra/docker/docker-compose.yaml"
    RunCommand("$compose down --rmi all --remove-orphans")
    RunCommand("$compose up --build --detach")
  }
  "test" {
    $compose = "docker compose --file ./infra/docker/docker-compose.test.yaml"
    RunCommand("$compose down --rmi all --remove-orphans")
    RunCommand("$compose up --build")
  }
}
