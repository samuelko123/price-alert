# Price Alert

A hobbyist project to send email notification on price drop on subscribed items.

## How to Run Locally

```
docker compose --file ./infra/docker/docker-compose.yaml --profile production up --build --remove-orphans
```

Then, go to http://localhost/

## How to Run Unit Test

```
docker compose --file ./infra/docker/docker-compose.yaml --profile test up --build --remove-orphans
```
