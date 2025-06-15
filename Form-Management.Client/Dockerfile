FROM node:alpine
WORKDIR /app
ARG VITE_USERS_URL

COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

EXPOSE 5000
CMD ["npm", "run", "preview", "--", "--port", "5000", "--host", "0.0.0.0"]