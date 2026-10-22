# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

Las convenciones del repositorio (estructura, estilo, commits, PR) están en `AGENTS.md`; léelo y respétalo. Resumen de lo esencial más abajo.

## Qué es este repositorio

Colección de mini retos educativos (15–30 min) para Oscar, en español. Cada reto vive en `retos/NNN-nombre/` con `README.md` (enunciado) y `PISTAS.md` (pistas). Los retos son proyectos independientes sin código compartido ni solución global; alternan tecnologías:

- 001 y 004: consola .NET 10 (`Program.cs` único; el 001 tiene el `.csproj` en la raíz del reto, el 004 dentro de `Potion-inventory/`).
- 002: proyecto Unity 6000.6.5f1 (`SistemaEventosPrioridades/`), código en `Assets/Scripts/` y pruebas NUnit en `Assets/Scripts/Tests/`.
- 003: C++17 (Visual Studio, toolset v143), `MovementUndoReUndo.slnx`.
- 005: Blazor Server .NET 10, `ReservaSalaApp/ReservaSalaApp.slnx` con tres proyectos: `ReservaSala.Data` (modelo), `ReservaSala.Application` (`SalaServices`, lógica de reservas) y `ReservaSalaApp` (web, `Components/Pages/Home.razor`).
- 006: C++17, analizador de logs (parseo y validación de líneas). Por ahora solo `README.md` y `PISTAS.md`; el proyecto de Visual Studio está pendiente.

## Comandos (desde la raíz, PowerShell)

```powershell
dotnet build retos/001-inventario-pociones/Potion-inventory.csproj
dotnet run --project retos/004-fabricacion-objetos/Potion-inventory/Potion-inventory.csproj
dotnet run --project retos/005-reserva-sala/ReservaSalaApp/ReservaSalaApp/ReservaSalaApp.csproj
```

- Unity (002): abrir la carpeta del proyecto en Unity; pruebas desde Test Runner en Edit Mode (nombres tipo `TrySchedule_ValidEvent_ReturnsTrue`). No hay build por CLI.
- C++ (003 y 006): compilar y ejecutar desde Visual Studio.
- Blazor (005): tras `dotnet run`, comprueba los casos en el navegador, incluidos rechazos y cancelaciones. `SalaServices` puede probarse sin la interfaz.
- No hay linter, formateador ni suite de pruebas global. Compilar no demuestra que los casos del enunciado funcionen: ejecuta y comprueba rechazos y operaciones que deben conservar el estado, e indica qué quedó sin verificar.

## Reglas de colaboración (importantes)

Definidas en `docs/AYUDA_Y_REVISION.md`:

- No muestres pistas ni soluciones salvo que Oscar las pida; entrega solo el siguiente nivel de `PISTAS.md`.
- Al revisar código, separa errores que impiden cumplir el enunciado de mejoras opcionales, y cierra proponiendo el siguiente reto con dificultad ajustada.
- Código completo solo cuando lo solicite.

## Añadir un reto

Usa `docs/PLANTILLA_RETO.md` y `docs/PLANTILLA_PISTAS.md`, nombra el directorio `NNN-nombre-descriptivo`, y actualiza los índices `README.md` y `retos/README.md`.

## Estilo

Mantén el estilo del archivo modificado (C#: 4 espacios, llaves en línea propia, `PascalCase` tipos/métodos, `camelCase` variables). Documentación en español. Commits breves de acción, centrados en un cambio; no incluyas binarios ni cachés del IDE (`bin/`, `obj/`, `x64/`).
