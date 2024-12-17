resource "docker_hub_repository" "api" {
  namespace   = "samuelko123"
  name        = "price-alert-api"
  description = "It is a hobbyist project"
  private     = false
}

resource "docker_hub_repository" "web" {
  namespace   = "samuelko123"
  name        = "price-alert-web"
  description = "It is a hobbyist project"
  private     = false
}

resource "docker_hub_repository" "reverse-proxy" {
  namespace   = "samuelko123"
  name        = "price-alert-reverse-proxy"
  description = "It is a hobbyist project"
  private     = false
}
