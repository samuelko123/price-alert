FROM nginx:1.27
COPY ./server.dev.conf /etc/nginx/conf.d/default.conf
