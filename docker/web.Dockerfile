FROM node:20-alpine AS base
RUN corepack enable
RUN mkdir /app
RUN chown node:node /app
WORKDIR /app
USER node

COPY package.json .
RUN pnpm install
COPY . .

################################################

FROM base AS test
ENTRYPOINT ["npm", "test"]

################################################

FROM base AS build
RUN npm run build

################################################

FROM node:20-alpine AS production
RUN corepack enable
RUN mkdir /app
RUN chown node:node /app
WORKDIR /app
USER node

COPY package.json .
COPY --from=build /app/.next /app/.next

ENV NODE_ENV=production
RUN pnpm install --prod
ENTRYPOINT ["npm", "start"]
