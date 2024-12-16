FROM nginx:1.27
COPY ./server.conf /etc/nginx/conf.d/default.conf
