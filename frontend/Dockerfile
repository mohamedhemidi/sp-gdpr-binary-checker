
FROM node:20 AS builder

WORKDIR /app

COPY . .

RUN npm install
RUN npm run build 


FROM nginx:latest
COPY ./conf/default.conf /etc/nginx/conf.d/default.conf
RUN rm -rf /usr/share/nginx/html

COPY --from=builder /app/dist/frontend/browser /usr/share/nginx/html
