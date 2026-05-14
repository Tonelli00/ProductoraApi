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

Herramientas usadas para los toast del sistema: https://sweetalert2.github.io/recipe-gallery/colored-toasts.html. 
Esta herramienta nos permite crear toast modernos, limpios y con mensajes personalizados de una manera muy facil. 

1-Hay que importalo en el archivo HTML que lo vayamos a usar. Para esto, importamos el siguiente script <script src="sweetalert2.min.js"></script>
2- Luego, para poder utilizarlo, se utiliza el objeto Swal con su metodo fire (Swal.fire) y luego se especifican los colores, mensajes, posición del modal en la página, logos y demás.


### Decisiones de negocio
Se optó por determinar que el usuario únicamente puede realizar una reserva para un asiento. El usuario no puede realizar una reserva con 2 asientos asociados. Ademaás, para realizar la reserva, el mismo tieme que estar logueado en la página. Caso contrario, no podrá ver el mapa de asientos.

### Herramienta para el control de concurrencia.
Se optó por hacer pruebas con la herramienta Ngrok. Esta herramienta sirve para exponer la API de una forma segura. De esta forma, se puede testear el compartamiento cuando dos personas intentan acceder al mismo recurso.
Los pasos para la instalación de dicha herramienta (Desde windows) son los siguientes:
1- Entrar al sitio web de ngrok: https://ngrok.com/
2- Crearnos una cuenta
3- Descargar el instalador de ngrok
4- Descomprimir el archivo .rar que nos da cuando ejecutamos el instalador
5- Ejecutar la aplicación
6- Configurar nuestro AuthToken. Para eso, ejecutamos el siguiente comando en la consola que se nos abre al ejecutar la aplicación: ngrok config add-authtoken $YOUR_AUTHTOKEN
7- Una vez configurado, levantamos la API de forma local y en otra consola ejecutamos http ngrok <puerto donde corre nuestra API>. 
8- Una vez realizado esto, queda nuestra API expuesta y se pueden realizar consultas desde distintos dispositivos.
