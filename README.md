# Minicore

### Materia: Ingenieria Web
### Por: José I. Escudero

## Instrucciones:
- Instalar paquetes NuGet (Visual Studio lo hace automaticamente)
- Ejecutar migraciones (en caso de hacerlo por linea de comando, revisar la seccion de "Migraciones" abajo)
- Configurar la conexion a la base de datos (ConnectionString) en `appsettings.json`.

## Migraciones:
Para ejecutar las migraciones por linea de comando, es necesario tener instalado el modulo de entity framework de la linea de comando `dotnet`.  
Para ello, ejecute el siguiente comando:  
```dotnet tool install --global dotnet-ef```

Comandos:
```
Crear migracion: dotnet ef migrations add <NAME> --project ./minicore
Ejecutar migraciones: dotnet ef database update --project ./minicore
Revertir migraciones: dotnet ef database update <nueva cabeza de migracion> --project ./minicore
```