# Repository Guidelines

## Estructura del proyecto

Este repositorio reúne retos educativos de C#/.NET, Unity, Blazor y C++. Cada reto vive en `retos/NNN-nombre/`, con su enunciado en `README.md` y las pistas en `PISTAS.md`.

- Los retos 001 y 004 contienen aplicaciones de consola .NET 10; el código está en sus respectivos `Program.cs`.
- El reto 002 contiene el proyecto Unity `SistemaEventosPrioridades/`: scripts en `Assets/Scripts/`, pruebas en `Assets/Scripts/Tests/` y escenas en `Assets/Scenes/`.
- El reto 003 contiene el proyecto C++ `MovementUndoReUndo/`.
- El reto 005 contiene la solución Blazor Server `ReservaSalaApp/` (.NET 10): `ReservaSala.Data` (modelo `Reserva`), `ReservaSala.Application` (`SalaServices` con la lógica de reservas y `Result`) y `ReservaSalaApp` (componentes en `Components/`, página principal en `Components/Pages/Home.razor`). La lógica de reservas no debe pasar a los componentes.
- El reto 006 (analizador de logs, C++17) solo tiene el enunciado y las pistas; Oscar creará el proyecto de Visual Studio al resolverlo.
- `docs/` contiene el flujo de trabajo, las reglas de ayuda y las plantillas.

Al añadir un reto, usa las plantillas y actualiza los índices `README.md` y `retos/README.md`.

## Compilación y ejecución

Ejecuta desde la raíz:

```powershell
dotnet build retos/001-inventario-pociones/Potion-inventory.csproj
dotnet run --project retos/004-fabricacion-objetos/Potion-inventory/Potion-inventory.csproj
dotnet run --project retos/005-reserva-sala/ReservaSalaApp/ReservaSalaApp/ReservaSalaApp.csproj
```

El primer comando compila el reto 001; el segundo ejecuta el 004 y el tercero arranca la aplicación Blazor del 005, que se prueba en el navegador. Todos requieren el SDK .NET 10.

Para C++, abre `retos/003-movimiento-deshacer-rehacer/MovementUndoReUndo/MovementUndoReUndo.slnx` en Visual Studio con las herramientas C++ v143. Compila y ejecuta desde el IDE. El reto 006 seguirá el mismo flujo cuando exista su proyecto.

Abre `retos/002-sistema-eventos-prioridades/SistemaEventosPrioridades/` con Unity 6000.6.5f1. No existe un comando global de compilación.

## Estilo y nombres

Mantén el estilo del archivo modificado. En C#, usa cuatro espacios, llaves en líneas separadas, `PascalCase` para tipos y métodos y `camelCase` para variables. Evita reformatear código ajeno al cambio. No hay formateador ni linter compartido configurado.

Escribe la documentación en español y conserva el patrón `NNN-nombre-descriptivo` para los directorios.

## Verificación

Ejecuta las pruebas NUnit del reto 002 desde el Test Runner de Unity en Edit Mode. Sigue nombres como `TrySchedule_ValidEvent_ReturnsTrue`. No hay un mínimo de cobertura establecido.

Para los demás retos, comprueba los casos del enunciado, incluidos rechazos y operaciones que deben conservar el estado. Indica qué ejecutaste y qué quedó pendiente; compilar no demuestra que todos los casos funcionen.

## Commits y pull requests

El historial usa mensajes breves de acción en español e inglés, como `Añade reto 4: fabricación de objetos`. Mantén cada commit centrado en un cambio.

En una pull request, identifica el reto, explica el comportamiento modificado y detalla la verificación realizada. Enlaza la incidencia si existe. Excluye binarios y cachés del IDE.

## Ayuda y revisión

Consulta `docs/AYUDA_Y_REVISION.md`: ofrece pistas y soluciones cuando Oscar las solicite. Entrega solo el siguiente nivel de pista y separa errores de mejoras opcionales al revisar código.
