FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# copia csproj e restaura as camadas
COPY *.csproj ./
RUN dotnet restore

# copia tudo e builda
COPY . ./
RUN dotnet publish "EasyInvoice.API.csproj" -c Release -o out

# builda com a imagem do runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

#ENTRYPOINT ["dotnet", "EasyInvoice.API.dll"]
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet EasyInvoice.API.dll