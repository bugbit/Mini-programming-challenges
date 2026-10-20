# Reto 4: Fabricación de objetos

**Tecnología:** aplicación de consola en C# con .NET\
**Tiempo estimado:** 30 minutos

## Enunciado

En un videojuego, el jugador fabrica objetos usando materiales de su inventario. Una espada, por ejemplo, requiere hierro y madera. Si falta alguno de esos materiales, el intento debe fallar sin consumir nada.

Implementa un sistema de fabricación que guarde las cantidades de objetos del inventario y permita utilizar distintas recetas. Cada receta indica los materiales necesarios, la cantidad de cada uno y el nombre del objeto que produce.

El sistema debe ofrecer estas operaciones:

- `TryCraft(recipe)`: intenta fabricar una unidad del objeto indicado en la receta. Devuelve `true` si completa la fabricación y `false` si faltan materiales.
- `GetQuantity(itemName)`: consulta la cantidad de un objeto sin modificar el inventario. Devuelve `0` si el objeto no está presente.

Puedes elegir las firmas concretas, cómo representar las recetas y cómo cargar el inventario inicial. No necesitas un menú interactivo: basta con demostrar las operaciones mediante llamadas desde `Main`.

## Reglas

- Una fabricación solo se acepta si hay cantidad suficiente de todos los materiales de la receta.
- Si se acepta, se descuentan las cantidades exactas de los materiales y se añade una unidad del objeto fabricado al inventario.
- Si el objeto fabricado ya existe, su cantidad aumenta en uno.
- Si falta algún material o su cantidad es insuficiente, `TryCraft` devuelve `false` y todo el inventario queda exactamente igual. Tampoco se añade el objeto fabricado.
- Ninguna cantidad puede quedar por debajo de cero.
- Cada llamada fabrica una sola unidad. Para fabricar varias, debes llamar varias veces a `TryCraft`.
- Las recetas se conservan sin cambios, tanto si el intento tiene éxito como si falla.
- Los nombres de objetos distinguen mayúsculas y minúsculas: `Hierro` y `hierro` son objetos distintos.
- Consultar un objeto ausente devuelve `0` sin añadirlo al inventario. Cuando una cantidad llega a cero por consumo, puedes conservar su entrada o eliminarla; la consulta debe devolver `0` en ambos casos.

Puedes suponer que los nombres no son nulos ni vacíos, las cantidades iniciales son enteros no negativos y cada receta tiene al menos un material con cantidad entera positiva. Un material aparece una sola vez dentro de cada receta y el objeto producido no figura entre sus materiales. Las cantidades caben en un `int` después de cada operación y todas las llamadas ocurren en un solo hilo.

El inventario no tiene límite de capacidad. No necesitas fabricar materiales automáticamente mediante otras recetas, guardar datos en disco ni implementar una interfaz gráfica.

## Ejemplo

La receta de `Espada` necesita `3` unidades de `Hierro` y `2` de `Madera`, y produce una unidad de `Espada`.

Con un inventario inicial de `5` unidades de `Hierro`, `1` de `Madera` y ninguna `Espada`, el intento devuelve `false`. El inventario sigue teniendo `5` de `Hierro`, `1` de `Madera` y ninguna `Espada`.

En otro inventario, comienza con `7` unidades de `Hierro`, `4` de `Madera` y una `Espada`. Ejecuta estas operaciones en orden:

| Operación | Resultado | Hierro posterior | Madera posterior | Espadas posteriores |
|---|---|---|---|---|
| `TryCraft(recetaEspada)` | `true` | 4 | 2 | 2 |
| `TryCraft(recetaEspada)` | `true` | 1 | 0 | 3 |
| `TryCraft(recetaEspada)` | `false` | 1 | 0 | 3 |
| `GetQuantity("Espada")` | `3` | 1 | 0 | 3 |
| `GetQuantity("Piedra")` | `0` | 1 | 0 | 3 |

Cualquier otro objeto del inventario conserva su cantidad durante estas operaciones.

## Comprueba estos casos

- Fabricar con las cantidades exactas: los materiales quedan a cero y aparece una unidad del objeto producido.
- Fabricar con materiales sobrantes y comprobar las cantidades restantes.
- Fabricar un objeto que ya existe y comprobar que su cantidad aumenta en uno.
- Rechazar un intento cuando un material tiene cantidad insuficiente, aunque los demás alcancen.
- Rechazar un intento cuando un material no existe. Comprueba que se conserva todo el inventario, incluido cualquier material suficiente que figure antes en la receta.
- Intentar fabricar con el inventario vacío.
- Repetir la fabricación hasta agotar los recursos y comprobar que el siguiente intento no cambia nada.
- Usar dos recetas que compartan un material y comprobar que ambas utilizan las cantidades actuales del mismo inventario.
- Comprobar que los objetos ajenos a la receta y las propias recetas no cambian.
- Consultar un objeto ausente y distinguir nombres como `Hierro` y `hierro`.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
