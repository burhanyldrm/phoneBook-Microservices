#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Contacts/Contact.Api/Contact.Api.csproj", "Services/Contacts/Contact.Api/"]
COPY ["Services/Contacts/Contact.Infrastructure/Contact.Infrastructure.csproj", "Services/Contacts/Contact.Infrastructure/"]
COPY ["Services/Contacts/Contact.Application/Contact.Application.csproj", "Services/Contacts/Contact.Application/"]
COPY ["Services/Contacts/Contact.Domain/Contact.Domain.csproj", "Services/Contacts/Contact.Domain/"]
RUN dotnet restore "Services/Contacts/Contact.Api/Contact.Api.csproj"
COPY . .
WORKDIR "/src/Services/Contacts/Contact.Api"
RUN dotnet build "Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.Api.dll"]
