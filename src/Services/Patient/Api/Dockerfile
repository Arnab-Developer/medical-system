﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY publish/app/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "MedicalSystem.Services.Patient.Api.dll"]