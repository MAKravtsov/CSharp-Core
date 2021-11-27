# ПРИМЕЧАНИЕ:
# 1) Запускается из каталога MicroService ( где лежит .sln )
# 2) Команда создания образа:
#       docker build \
#           -t (Название образа) \
#           -f (Путь к образу, если он не в вызывающем каталоге) \
#           --build-arg service_name=(наименование микросервиса, если задаем его через конфиг) \
#           .
# 3) Команда создания контейнера
#       docker run \
#           --name (наименование контейнера) \
#           -p (локальный порт):(порт в контейнере) \
#           -v (Volumes - что-то там) \
#           (наименование образа)

# Входим в образ sdk
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS sdk

# Копируем файлы с каталога, из которого запускаем ( . ), в каталог /src в образе sdk
COPY . /src

# переходим в каталог src/ProductCatalog в образе sdk ( аналогично команде cd )
WORKDIR "/src/ProductCatalog.RestApi"

# Делаем Build проекта
RUN dotnet build "ProductCatalog.RestApi.csproj" -c Release -o /app/build

#  Делаем publish проекта
RUN dotnet publish "ProductCatalog.RestApi.csproj" -c Release -o /app/publish

# Входим в образ asp .net
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS aspnet

# переходим там в каталог /app
WORKDIR /app

# копируем файлы из образа sdk ( --from ) в образ asp .net
COPY --from=sdk /app/publish .

# прослушиваемый порт
EXPOSE 80

# Вызов команды dotnet ProductCatalog.dll ( можно было через команду CMD )
ENTRYPOINT ["dotnet", "ProductCatalog.RestApi.dll"]
