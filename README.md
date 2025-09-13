---

#  NatureAPI – Examen Parcial

API REST desarrollada en **.NET 8** para gestionar **lugares naturales de México** (parques, cascadas, miradores, senderos), incluyendo coordenadas, fotos, reseñas y amenidades.

##  Requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Docker](https://www.docker.com/)
* [SQL Server en Docker](https://hub.docker.com/_/microsoft-mssql-server)

---

##  Configuración del proyecto

### 1️ Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/NatureAPI.git
cd NatureAPI
```

### 2️ Levantar SQL Server con Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" `
   -p 1433:1433 --name sqlnature -d mcr.microsoft.com/mssql/server:2022-latest
```

 Usuario: `sa`
 Password: `Your_password123`
 Puerto: `1433`

---

### 3️ Configurar la conexión en `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=NatureDB;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
}
```

---

### 4️ Ejecutar migraciones y seed

```bash
dotnet ef database update
```

Esto creará las tablas y agregará los **datos iniciales (seed)**:

* Lugares (Place)
* Senderos (Trail)
* Fotos (Photo)
* Amenidades (Amenity)

---

### 5️ Levantar la API

```bash
dotnet run
```

La API quedará disponible en:
 `http://localhost:5001/api/places`

---

##  Entidades principales

* **Place** → Lugar natural principal
* **Trail** → Senderos asociados a un lugar
* **Photo** → Fotos relacionadas al lugar
* **Review** → Reseñas de visitantes
* **Amenity** → Amenidades de un lugar
* **PlaceAmenity** → Tabla puente para relación N–N

Relaciones:

* Place (1) — (N) Trail
* Place (1) — (N) Photo
* Place (1) — (N) Review
* Place (N) — (N) Amenity

---

##  Endpoints principales

###  Obtener todos los lugares (con filtros opcionales)

```http
GET /api/places?category=Cascada&difficulty=Alta
```

###  Obtener un lugar por ID

```http
GET /api/places/1
```

###  Crear un nuevo lugar

```http
POST /api/places
Content-Type: application/json

{
  "name": "Nuevo Parque",
  "description": "Un lugar hermoso",
  "category": "Parque",
  "latitude": 19.4326,
  "longitude": -99.1332,
  "elevationMeters": 2200,
  "accessible": true,
  "entryFee": 50,
  "openingHours": "08:00-18:00"
}
```

---

##  Criterios cumplidos

✔ Diseño de entidades y relaciones con EF Core
✔ Migraciones en SQL Server con Docker
✔ Datos precargados (Seed) coherentes
✔ Endpoints REST solicitados
✔ Código estructurado en capas
✔ Documentación en README

---

##  Licencia

Este proyecto fue desarrollado como parte del **Examen Parcial de Desarrollo con .NET 8**.

---
