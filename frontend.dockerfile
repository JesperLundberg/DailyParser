FROM node:lts AS build
WORKDIR /app

COPY src/FrontEnd/ .

RUN npm i
RUN npm run build

# Final container

FROM httpd:2.4-alpine AS runtime

COPY --from=build /app/dist /usr/local/apache2/htdocs/

EXPOSE 80

# FROM node:lts AS runtime
# WORKDIR /app
#
# COPY src/FrontEnd/ .
#
# RUN npm i
# RUN npm run build
#
# ENV HOST=0.0.0.0
# ENV PORT=80
# EXPOSE 80
# CMD node ./dist/server/entry.mjs
