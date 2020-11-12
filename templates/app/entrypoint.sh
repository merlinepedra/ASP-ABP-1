#!/bin/bash

set -e

cd /app/MyProjectName/aspnet-core/src/MyProjectName.DbMigrator

until dotnet run; do
echo "SQL Server is starting up"
sleep 1
done

echo "SQL Server is up"
cd /app/MyProjectName/aspnet-core/src/MyProjectName.HttpApi.Host
dotnet run
