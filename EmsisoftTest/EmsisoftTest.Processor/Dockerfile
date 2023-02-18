FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
LABEL stage=builder
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as builder

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "EmsisoftTest.Processor.dll"]