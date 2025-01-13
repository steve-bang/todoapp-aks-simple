# Step 1: Use an official Node.js image as a base image
FROM node:16 AS build

# Step 2: Set the working directory inside the container
WORKDIR /app

# Step 3: Copy the package.json and package-lock.json (or yarn.lock)
COPY package*.json ./

# Step 4: Install the dependencies
RUN npm install

# Step 5: Copy the rest of the app's source code into the container
COPY . .

# Step 6: Build the React app
RUN npm run build

# Step 7: Use an official Nginx image to serve the build files
FROM nginx:alpine

# Step 8: Copy the build output from the first stage into the Nginx container
COPY --from=build /app/build /usr/share/nginx/html

# Step 9: Expose the port that Nginx will run on
EXPOSE 80

# Step 10: Start Nginx server
CMD ["nginx", "-g", "daemon off;"]

# docker build --platform linux/amd64 -t mrstevebang/todoapp-webapp:tag .