# Reto 3: Movimiento con deshacer y rehacer

**Tecnología:** aplicación de consola en C++17\
**Tiempo estimado:** 30 minutos

## Enunciado

Estás creando una herramienta para colocar personajes en un mapa. El usuario puede mover un personaje y corregir sus decisiones con las operaciones de deshacer y rehacer.

Implementa un sistema que mantenga la posición de un personaje dentro de un tablero de 5 × 5 casillas y el historial de sus movimientos. El personaje comienza en `(0, 0)`.

El sistema debe ofrecer estas operaciones:

- `Move(dx, dy)`: intenta desplazar al personaje. Devuelve `true` si acepta el movimiento y `false` si lo rechaza.
- `Undo()`: deshace el último movimiento que siga aplicado. Devuelve `true` si puede deshacer y `false` si no hay ninguno.
- `Redo()`: vuelve a aplicar el último movimiento deshecho que todavía pueda recuperarse. Devuelve `true` si puede rehacer y `false` si no hay ninguno.
- Consultar la posición actual sin modificarla.

Puedes elegir las firmas concretas y cómo mostrar los resultados en consola. No necesitas un menú interactivo: basta con demostrar las operaciones mediante llamadas desde `main`.

## Reglas

- Las coordenadas válidas van de 0 a 4, ambos incluidos, en cada eje.
- Un movimiento solo es válido si avanza una casilla en una dirección: `(1, 0)`, `(-1, 0)`, `(0, 1)` o `(0, -1)`.
- Rechaza los movimientos diagonales, los desplazamientos de más de una casilla, `(0, 0)` y cualquier movimiento que salga del tablero.
- Cada movimiento aceptado se puede deshacer una vez mientras siga aplicado. Deshacer varios movimientos recorre el historial desde el más reciente.
- Rehacer recupera los movimientos en el orden necesario para reconstruir el recorrido anterior. Por ejemplo, si deshaces primero B y después A, debes rehacer primero A y después B.
- Aceptar un movimiento nuevo después de deshacer descarta todos los movimientos que quedaban disponibles para rehacer.
- Un movimiento rechazado conserva la posición y todo el historial, incluidos los movimientos disponibles para rehacer.
- Si `Undo` o `Redo` devuelven `false`, el estado no cambia.
- Deshacer y rehacer no cuentan como movimientos nuevos ni deben duplicar entradas en el historial.

Puedes suponer que `dx` y `dy` son enteros y que todas las llamadas ocurren en un solo hilo. No necesitas guardar el historial en disco ni limitar su tamaño.

## Ejemplo

Partiendo de `(0, 0)`, ejecuta estas operaciones en orden:

| Operación | Resultado | Posición posterior |
|---|---|---|
| `Move(1, 0)` | `true` | `(1, 0)` |
| `Move(0, 1)` | `true` | `(1, 1)` |
| `Move(1, 0)` | `true` | `(2, 1)` |
| `Undo()` | `true` | `(1, 1)` |
| `Undo()` | `true` | `(1, 0)` |
| `Redo()` | `true` | `(1, 1)` |
| `Move(1, 1)` | `false` | `(1, 1)` |
| `Redo()` | `true` | `(2, 1)` |
| `Undo()` | `true` | `(1, 1)` |
| `Move(-1, 0)` | `true` | `(0, 1)` |
| `Redo()` | `false` | `(0, 1)` |

El movimiento diagonal rechazado conserva la posibilidad de rehacer. El último movimiento aceptado crea un recorrido distinto y descarta el movimiento que estaba pendiente de rehacer.

## Comprueba estos casos

- Consultar la posición inicial y llamar a `Undo` y `Redo` sin movimientos previos.
- Mover en las cuatro direcciones desde posiciones que lo permitan.
- Llegar a cada borde y rechazar un movimiento que intente atravesarlo.
- Rechazar diagonales, saltos y movimientos sin desplazamiento sin cambiar el estado.
- Deshacer todos los movimientos hasta volver a `(0, 0)` y rechazar un deshacer adicional.
- Rehacer todo el recorrido y rechazar un rehacer adicional.
- Alternar `Undo` y `Redo` varias veces sobre el mismo movimiento sin duplicarlo.
- Deshacer varios movimientos, rehacer solo uno y aceptar un movimiento distinto: los demás ya no deben poder rehacerse.
- Rechazar un movimiento después de deshacer y comprobar que aún puedes rehacer. Comprueba tanto un desplazamiento inválido como una salida del tablero.
- Volver a una casilla ya visitada mediante movimientos nuevos y comprobar que cada paso conserva su lugar en el historial.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
