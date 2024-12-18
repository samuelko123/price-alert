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
RUN pnpm build
RUN pnpm prune --prod

################################################

FROM node:20-alpine AS production
WORKDIR /app
USER node

ENV NODE_ENV=production

COPY --from=build /app/package.json /app/package.json
COPY --from=build /app/node_modules/ /app/node_modules/
COPY --from=build /app/.next/ /app/.next/

ENTRYPOINT ["npm", "start"]
