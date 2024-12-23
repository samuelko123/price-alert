#!pwsh
param(
  [Parameter(Position = 0, Mandatory = $true)]
  [ValidateSet("dev", "prod")]
  [string]$Profile
)

switch ($Profile) {
  "dev" {
    Invoke-Expression "docker compose --file ./infra/docker/docker-compose.yaml --file ./infra/docker/docker-compose.dev.yaml up --build --remove-orphans"
  }
  "prod" {
    Invoke-Expression "docker compose --file ./infra/docker/docker-compose.yaml up --build --remove-orphans"
  }
}
