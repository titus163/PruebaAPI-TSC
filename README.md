# PruebaAPI-TSC
Web API con ASP.Net Core 5.0 y EF como prueba para TSC de GT

1. Instalacion:

Se agrego iso3166.bak que contiene la data para el proyecto. Es un archivo de base de datos para sql server 2017.
El pproyecto se ecribio con VisualStudio 2019. Es un web api co asp.net core 5.0 y EF Core. Incluye metodos CRUD para los controllers Countries, States and Cities.

2. Uso del API
El Web API tiene 3 controller. Uno para Cities, otro para States y Countries. Cada controller tiene metodos CRUD completos. Un Metodo Get paginado que permite recuperar datos con funcion de ordenamiento y paginacion personalizada. Swagger permite visualizar toda el API y ejecutar las pruebas en una forma sencilla y practica.

Tambien incluye los esquemas de datos necesarios para poder hacer uso de los metodos para agregar nuevos registros y/o borrarlos.

El metodo de borrado funciona de forma especifica para Controller, por ejemplo para borrar una ciudad se especifica como parametro el nombre de la ciudad. El APi hara una busquedad exacta y de encontrar el registro lo borrara.

El metodo de borrado para el controller States funciona diferente. Se especifica el nombre del State(Subdivision), el sistema lo busca, luego busca todos los Cities (Child Records) y primero borrar los Cities si los hay y luego borra el State.

El metodo de borrado para Countries funciona en forma similar. Filtra por el codigo ISO3 (O sea, el codigo de 3 caracteres para cada Country) y luego busca en orden de suburdinacion los States y de cada States los Cities. De esta forma borra primero los Cities, luego los States y finalmente el Country.

Los esquemas para los Metodos POST permiten poder agregar registros nuevos para un Country, States y Cities. Para Agregar un Citie es necesario conocer el StateId al que pertenece y al agregar un nuevo State es necesario conocoer el CountryId para poder ingresarlo.

3. Seguridad
El mecanismo de seguridad por medio de JWT no esta incluido, por falta de tiempo ya no logre incluir la autenticacion con JWT Tokens.

Notas adicionales:
La base de datos incluye toda la data para la estructura Countries-States-Cities segun el estandar ISO 3166. Las tablas estan pre-cargadas con los correspondientes datos. Por esa razon no se implementaron metodos Seed para el DBContext. Entre los archivos de este projecto encontrara tambien los JSON que contienen los datos que sirvieron para construir la Base de Datos SQL que se incluyo en este proyecto.


