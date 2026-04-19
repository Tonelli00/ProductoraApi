# ProyectoSoft-TP2026

<H1>Diagrama de la base de datos</H1>

<img width="783" height="678" alt="imagen" src="https://github.com/user-attachments/assets/88c650ea-9801-4b13-a93d-5a578649c4bc" />


<H1>Comandos para migrar y actualizar la base de datos</H1>

Primero, en caso de que tengamos la imagen de la Base de datos en docker, hay que asegurarnos de que el contenedor este levantado. Para eso se ejecuta: 
1 - docker ps (Para ver si es que ya esta levantado el contenedor)
2 - docker start <nombrecontenedor> (Para levantar el contenedor)

Luego, hay que cambiar la cadena "DefaultConnection" en appsettings.json para conectarte con tu gestor de bases de datos.

<H2> Migración de la base de datos</H2>

Hay 2 alternativas:
1 - Desde el visual studio: En este caso tenemos que abrir la Consola del Administrador de Paquetes y cambiar el proyecto predeterminado a "Infrastructure". Luego, se ejectuta el siguiente comando Add-Migration <NombreMigración>
2 - Desde la consola: En este caso nos tenemos que parar en la carpeta de la API (Proyecto predeterminado) y ejecutar el siguiente comando: dotnet ef migrations add <NombreMigración> --project Infrastructure --startup-project Productora 
En ambas alternativas se creará una carpeta nueva llamada "Migrations".

<H2> Actualización de la base de datos</H2>

Hay 2 alternativas:
1 - Desde el visual studio: En este caso tenemos que abrir la Consola del Administrador de Paquetes y cambiar el proyecto predeterminado a "Infrastructure". Luego, se ejectuta el siguiente comando Update-Database
2 - Desde la consola: En este caso nos tenemos que parar en la carpeta de la API (Proyecto predeterminado) y ejecutar el siguiente comando: dotnet ef database update --project Infrastructure --startup-project Productora
