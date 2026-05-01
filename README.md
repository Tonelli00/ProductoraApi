#  ProyectoSoft - TP 2026

##  Diagrama de Base de Datos

![Diagrama de Base de Datos](https://github.com/user-attachments/assets/88c650ea-9801-4b13-a93d-5a578649c4bc)

---

##  Configuración inicial

Antes de comenzar con las migraciones, asegurarse de que la base de datos esté levantada.

### Nosotros, decidimos usar  Docker

1. Verificar si el contenedor está activo:
```bash
docker ps
```

2. En caso de que no esté corriendo, iniciarlo:
```bash
docker start <nombre_del_contenedor>
```

---

### Una vez levantado el contenedor, hay que configurar la cadena de conexión

Modificar la cadena `DefaultConnection` en el archivo `appsettings.json` para que apunte correctamente a tu gestor de base de datos.

---

## Una vez lista la cadena de conexión, realizmos la migracion de Base de Datos

Existen dos formas de generar migraciones:

### - Opción 1: Desde Visual Studio

1. Abrir la Consola del Administrador de Paquetes  
2. Seleccionar el proyecto **Infrastructure** como predeterminado  
3. Ejecutar:

```powershell
Add-Migration <NombreMigracion>
```

---

### - Opción 2: Desde la terminal

1. Posicionarse en la carpeta del proyecto API
2. Ejecutar dotnet restore
3. Ejecutar:

```bash
dotnet ef migrations add <NombreMigracion> --project Infrastructure --startup-project Productora
```

En ambos casos, se generará automáticamente la carpeta `Migrations/`.

---

## Actualización de la Base de Datos

Una vez creada la migración, se debe aplicar a la base de datos.

### - Opción 1: Desde Visual Studio

1. Abrir la Consola del Administrador de Paquetes  
2. Seleccionar el proyecto **Infrastructure**  
3. Ejecutar:

```powershell
Update-Database
```

---

### - Opción 2: Desde la terminal

```bash
dotnet ef database update --project Infrastructure --startup-project Productora
```

---

## Decisiones de Arquitectura

###  Backend

Se implementó una arquitectura inspirada en MediatR, donde cada caso de uso se maneja mediante su propio Handler.

Esto permite:
- Separación de responsabilidades  
- Mayor mantenibilidad  
- Escalabilidad del sistema  

---

###  Frontend

Se optó por una estructura modular basada en vistas y componentes reutilizables, priorizando:

- Organización del código  
- Separación entre lógica y presentación  
- Facilidad de mantenimiento  

### Decisiones de negocio
Se optó por determinar que el usuario únicamente puede realizar una reserva para un asiento. El usuario no puede realizar una reserva con 2 asientos asociados. Ademaás, para realizar la reserva, el mismo tieme que estar logueado en la página. Caso contrario, no podrá ver el mapa de asientos.


