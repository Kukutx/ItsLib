FROM docker.io/node:20.7-bookworm as build
 
WORKDIR /src
 
COPY package.json /src/package.json
COPY package-lock.json /src/package-lock.json
 
RUN npm ci
RUN npm install -g @angular/cli
 
COPY . /src
 
RUN ng build
 
FROM docker.io/nginx:1.24
 
EXPOSE 80
 
COPY default.conf /etc/nginx/conf.d/default.conf
 
COPY --from=build /src/dist/itslibfrontend/* /usr/share/nginx/html/



