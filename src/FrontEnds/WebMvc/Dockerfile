﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY publish/app/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "MedicalSystem.FrontEnds.WebMvc.dll"]