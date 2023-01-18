FROM node:19-alpine AS build
WORKDIR /frontend
COPY package*.json ./
RUN npm install --legacy-peer-deps
COPY . .
RUN npm run build