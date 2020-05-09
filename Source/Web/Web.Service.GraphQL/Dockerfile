FROM microsoft/aspnetcore-build
LABEL author="Irina Nalijaona"
ENV ASPNETCORE_URLS=http://*:5000
WORKDIR /app
EXPOSE 5000
CMD ["/bin/bash", "-c", "dotnet restore && dotnet run"]
