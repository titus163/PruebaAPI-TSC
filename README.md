# PruebaAPI-TSC
<div class="flex-wrap" style="background-color: aliceblue">
  <div class="card rounded">
    <h3>Web API con ASP.Net Core 5.0 y EF como prueba para TSC de GT</h3>
    <ul>
      <li><strong>1. Instalacion</strong>:</li>
      <p>Se agrego iso3166.bak que contiene la data para el proyecto. Es un archivo de base de datos para sql server 2017.
        El pproyecto se ecribio con VisualStudio 2019. Es un web api co asp.net core 5.0 y EF Core. Incluye metodos CRUD para los controllers Countries, States and Cities.</p>
      <li><strong>2. Uso del API</strong></li>
      <p>El Web API tiene 3 controller. Uno para Cities, otro para States y Countries. Cada controller tiene metodos CRUD completos. Un Metodo Get paginado que permite recuperar datos con funcion de ordenamiento y paginacion personalizada. Swagger permite visualizar toda el API y ejecutar las pruebas en una forma sencilla y practica.</p>
      <p>Tambien incluye los esquemas de datos necesarios para poder hacer uso de los metodos para agregar nuevos registros y/o borrarlos.</p>
      <p>El metodo de borrado funciona de forma especifica para Controller, por ejemplo para borrar una ciudad se especifica como parametro el nombre de la ciudad. El APi hara una busquedad exacta y de encontrar el registro lo borrara.</p>
      <p>El metodo de borrado para el controller States funciona diferente. Se especifica el nombre del State(Subdivision), el sistema lo busca, luego busca todos los Cities (Child Records) y primero borrar los Cities si los hay y luego borra el State.</p>
      <p>El metodo de borrado para Countries funciona en forma similar. Filtra por el codigo ISO3 (O sea, el codigo de 3 caracteres para cada Country) y luego busca en orden de suburdinacion los States y de cada States los Cities. De esta forma borra primero los Cities, luego los States y finalmente el Country.</p>
      <p>Los esquemas para los Metodos POST permiten poder agregar registros nuevos para un Country, States y Cities. Para Agregar un Citie es necesario conocer el StateId al que pertenece y al agregar un nuevo State es necesario conocoer el CountryId para poder ingresarlo.</p>
      <p></p>
      <li><strong>3. Seguridad</strong></li>
      <p>El mecanismo de seguridad por medio de JWT esta incluido. Para recuperar un JWT Token invoque el metodo LoginUser usando como parametros: <strong>"Username"</strong>: <u>"test@domain.com"</u>, <strong>"Password"</strong>: <u>"abc123"</u>.</p>
    </ul>
    <h3>Notas adicionales:</h3>
    <p>La base de datos incluye toda la data para la estructura Countries-States-Cities segun el estandar ISO 3166. Las tablas estan pre-cargadas con los correspondientes datos. Por esa razon no se implementaron metodos Seed para el DBContext. Entre los archivos de este projecto encontrara tambien los JSON que contienen los datos que sirvieron para construir la Base de Datos SQL que se incluyo en este proyecto.</p>
    <p>El metodo Get predeterminado de los controllers Countries, States, Cities es un metodo que implementa paginación, ordenamiento y filtrado de registros, todo en un unico metodo. Usando la el parametro SearchCriteria puede hacer busquedas especificas y reducir el conjunto de resultados.</p>
  </div>
</div>


