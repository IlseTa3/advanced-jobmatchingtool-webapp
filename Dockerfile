# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY advanced-jobmatchingtool-webapp/*.csproj advanced-jobmatchingtool-webapp/
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# Force IPv4 over IPv6
RUN echo "precedence ::ffff:0:0/96  100" >> /etc/gai.conf
#dummy change
#rebuild trigger
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "advanced-jobmatchingtool-webapp.dll"]
