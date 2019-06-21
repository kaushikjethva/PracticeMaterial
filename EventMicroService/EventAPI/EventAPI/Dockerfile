#stage1
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build
RUN dotnet publish -c Release -o out

#Stage2
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as runtime
WORKDIR /app
COPY --from=build /src/out .
EXPOSE 80
ENV ConnectionStrings:SqlConnecton="Constr"
ENV Jwt:Secret="Thisismylengthsecretkeyforencryption"
ENV Jwt:Issuer="http://localhost:5000"
ENV Jwt:Audience="http://localhost:5001"
ENTRYPOINT [ "dotnet" ,"EventAPI.dll"]
