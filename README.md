# Price Alert (WIP)

A hobbyist project to send email notification on price drop on subscribed items.  
You can visit https://price-alert.samuelko123.dev/.

## How to Run Locally

```
docker compose --file ./infra/docker/docker-compose.yaml --profile production up --build --remove-orphans
```

Then, go to http://localhost:4000/

## How to Run Unit Test

```
docker compose --file ./infra/docker/docker-compose.yaml --profile test up --build --remove-orphans
```
