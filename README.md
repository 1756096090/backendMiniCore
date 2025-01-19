# BackendMiniCore API

**BackendMiniCore API** es un proyecto de API web desarrollado en .NET Core, que proporciona integración con MongoDB para la persistencia de datos. Ofrece endpoints RESTful para la gestión de departamentos, empleados, seguimiento de gastos y pronósticos del tiempo.

## Características

- **Integración con MongoDB** para la persistencia de datos.
- **Soporte CORS** para solicitudes de origen cruzado.
- **Documentación Swagger/OpenAPI** para una fácil exploración de la API.
- Endpoints RESTful para:
  - Gestión de **departamentos**.
  - Registros de **empleados**.
  - Seguimiento de **gastos**.
  - **Pronósticos del tiempo**.


## ⚙️ Configuración de MongoDB

1. **MongoDBSettings.cs**: 
   - Se encuentra en `Config/MongoDBSettings.cs`.
   - Contiene los atributos necesarios para la conexión a MongoDB como la cadena de conexión y el nombre de la base de datos.

2. **MongoDBContext.cs**:
   - Se encuentra en `Config/MongoDBContext.cs`.
   - Proporciona la configuración del contexto de MongoDB, gestionando la conexión a la base de datos.

3. **Registro en Program.cs**:
   - En `Program.cs` se configura el registro de `MongoDBSettings` y `MongoDBContext` mediante inyección de dependencias:
   
   ```csharp
   builder.Services.Configure<MongoDBSettings>(
       builder.Configuration.GetSection("MongoDBSettings"));

   builder.Services.AddSingleton<MongoDBContext>();
   builder.Services.AddSingleton<IMongoDatabase>(sp => 
   {
       var context = sp.GetService<MongoDBContext>();
       return context.GetDatabase();
   });
