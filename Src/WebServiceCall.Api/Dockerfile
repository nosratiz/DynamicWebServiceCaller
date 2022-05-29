FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Src/WebServiceCall.Api/WebServiceCall.Api.csproj", "WebServiceCall.Api/"]
RUN dotnet restore "Src/WebServiceCall.Api/WebServiceCall.Api.csproj"
COPY . .
WORKDIR "/src/WebServiceCall.Api"
RUN dotnet build "WebServiceCall.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebServiceCall.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServiceCall.Api.dll"]
