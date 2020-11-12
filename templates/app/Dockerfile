FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN mkdir /app
COPY entrypoint.sh /app
WORKDIR /app
ENV PATH "$PATH:/root/.dotnet/tools"
RUN dotnet dev-certs https --trust
RUN dotnet dev-certs https -ep /app/certs/dev-cert.pfx -p SecretPassword
#ENV ASPNETCORE_ENVIRONMENT=Development
#ENV DOTNET_USE_POLLING_FILE_WATCHER=true
RUN dotnet tool install -g dotnet-ef
RUN dotnet tool install -g Volo.Abp.Cli
RUN abp new MyProjectName -cs "Server=db;Database=MyProjectName;User Id=sa;password=myPassw0rd" -u angular --csf
# temp
RUN apt-get update
RUN apt-get install vim
# temp
EXPOSE 80
EXPOSE 443
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
