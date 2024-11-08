# using the official aspnet image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# switching from root to non-root user
USER app

# setting the working directory
WORKDIR /app

# exsposing ports FOR OTHER CONTAINERS (not outside)
EXPOSE 8080
EXPOSE 8081

# we use multi-stage builds to reduce the size of the final image
# we use the sdk image to build the app
# but then sdk will not be included in the final image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# argument with default value of release
ARG BUILD_CONFIGURATION=Release

# setting the working directory
# every stage has its own working directory
WORKDIR /src

# copying the csproj file
# copy creates a folder if one does not exist
COPY ["./Rain.API/Rain.API.csproj", "Rain.API/"]

# restoring the dependencies
RUN dotnet restore "./Rain.API/Rain.API.csproj"

# copying the rest of the files
COPY . .

# switching to a directory where we will build the app
WORKDIR "/src/Rain.API"

# building the app
# -c $BUILD_CONFIGURATION -- build configuration
# -o /app/build -- output directory
RUN dotnet build "./Rain.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# publishing the app
FROM build AS publish

# argument with default value of release
ARG BUILD_CONFIGURATION=Release

# publishing the app
RUN dotnet publish "./Rain.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# final stage
FROM base AS final

# setting the working directory
WORKDIR /app

# copying the published app
COPY --from=publish /app/publish .

# running the app
ENTRYPOINT ["dotnet", "Rain.API.dll"]

# docker build [OPTIONS] PATH | URL | - .
# -t name and optionally a tag in the 'name:tag' format
# -f path to the Dockerfile

#docker run -p 8080:8080 -d rain-docker-test:v1
#docker build -t rain-docker-test:v1 -f "./Rain.API/Dockerfile" .