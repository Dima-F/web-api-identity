## Build
docker build -t mywebapi .

## Start
docker run -p "8080:8080" mywebapi

## Explanaition for Dockerfile

Using multiple stages in a Dockerfile allows you to optimize the final image by including only what is necessary to run the application. In your example:

    The build stage is used to compile the .NET application.

    The final stage is used to create a lightweight runtime image with the compiled application.

This approach is a best practice for building production-ready Docker images.