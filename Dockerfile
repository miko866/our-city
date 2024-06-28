FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL maintainer="OurCity"

WORKDIR /app
COPY /build . 

RUN mkdir /sql
COPY /sql /sql

RUN mkdir /storage
COPY /storage /storage

#Set Timezone
ENV TZ="Europe/Bratislava"

EXPOSE 8080
EXPOSE 443

ENTRYPOINT ["dotnet", "Server.dll"]
