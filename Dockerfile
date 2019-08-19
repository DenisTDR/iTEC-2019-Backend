FROM microsoft/dotnet:2.2-sdk as build-env
WORKDIR /app/

COPY ./API.Base/API.Base.Web.Base/API.Base.Web.Base.csproj /app/src/API.Base/API.Base.Web.Base/API.Base.Web.Base.csproj
COPY ./API.Base/API.Base.Logging/API.Base.Logging.csproj /app/src/API.Base/API.Base.Logging/API.Base.Logging.csproj
COPY ./API.Base/API.Base.Emailing/API.Base.Emailing.csproj /app/src/API.Base/API.Base.Emailing/API.Base.Emailing.csproj
COPY ./API.Base/API.Base.Files/API.Base.Files.csproj /app/src/API.Base/API.Base.Files/API.Base.Files.csproj
COPY ./API.Base/API.Base.Web.Common/API.Base.Web.Common.csproj /app/src/API.Base/API.Base.Web.Common/API.Base.Web.Common.csproj
COPY ./API.StartApp/API.StartApp.csproj /app/src/API.StartApp/API.StartApp.csproj

WORKDIR /app/src/API.StartApp/

RUN dotnet restore


COPY ./ /app/src

RUN dotnet publish --output "/app/bin" --configuration release 


FROM microsoft/dotnet:2.2-aspnetcore-runtime as runtime-env
RUN apt-get update && apt-get install -y \
    sudo \
    net-tools \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /app/bin
EXPOSE 5030
ENV NETCORE_USER_UID 69

COPY docker-entrypoint.sh /usr/bin/docker-entrypoint.sh
RUN chmod +x /usr/bin/docker-entrypoint.sh
CMD ["docker-entrypoint.sh"]

COPY --from=build-env /app/bin /app/bin

