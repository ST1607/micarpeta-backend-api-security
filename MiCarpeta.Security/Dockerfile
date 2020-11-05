#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MiCarpeta.Security/MiCarpeta.Security.Presentation.csproj", "MiCarpeta.Security/"]
COPY ["MiCarpeta.Security.Domain/MiCarpeta.Security.Domain.csproj", "MiCarpeta.Security.Domain/"]
COPY ["MiCarpeta.Security.Domain.Entities/MiCarpeta.Security.Domain.Entities.csproj", "MiCarpeta.Security.Domain.Entities/"]
COPY ["MiCarpeta.Security.Repository/MiCarpeta.Security.Repository.csproj", "MiCarpeta.Security.Repository/"]
COPY ["MiCarpeta.Security.Common/MiCarpeta.Security.Common.csproj", "MiCarpeta.Security.Common/"]
COPY ["MiCarpeta.Security.Application/MiCarpeta.Security.Application.csproj", "MiCarpeta.Security.Application/"]
RUN dotnet restore "MiCarpeta.Security/MiCarpeta.Security.Presentation.csproj"
COPY . .
WORKDIR "/src/MiCarpeta.Security"
RUN dotnet build "MiCarpeta.Security.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiCarpeta.Security.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiCarpeta.Security.Presentation.dll"]