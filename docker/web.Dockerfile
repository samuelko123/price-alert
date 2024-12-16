FROM node:20-alpine AS base

RUN mkdir /app
RUN chown node:node /app

WORKDIR /app
USER node

COPY package.json .
RUN yarn install
COPY . .

FROM base AS test
ENTRYPOINT ["npm", "test"]
