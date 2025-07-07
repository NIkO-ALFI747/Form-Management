FROM node:alpine
WORKDIR /app

ARG VITE_USERS_URL
ARG VITE_LOGIN_URL
ARG VITE_SIGNUP_URL
ARG VITE_SIGNOUT_URL

COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

EXPOSE 5000
CMD ["npm", "run", "preview", "--", "--port", "5000", "--host", "0.0.0.0"]