# Reto 1: Inventario de pociones

**Tecnología:** aplicación de consola en C# con .NET  
**Tiempo estimado:** 15–30 minutos

## Enunciado

Vas a implementar un inventario formado por un número fijo de huecos. Cada hueco puede estar vacío o guardar una pila de un solo tipo de objeto. Cada pila tiene un límite máximo de unidades.

Crea una operación llamada `TryAdd`. Recibe el inventario, el tipo de objeto y la cantidad que quieres añadir. Devuelve `true` si consigue guardar toda la cantidad y `false` si no cabe.

Al añadir objetos, sigue este orden:

1. Completa primero las pilas existentes del mismo tipo.
2. Si todavía quedan unidades, usa los huecos vacíos.
3. Si no cabe toda la cantidad, la operación falla y el inventario queda exactamente como estaba antes.
4. La cantidad debe ser mayor que cero.

## Ejemplo

Supón que el inventario tiene **3 huecos** y cada pila admite **5 unidades**:

| Hueco | Contenido |
|---|---|
| 1 | 3 pociones |
| 2 | Vacío |
| 3 | 2 monedas |

Si intentas añadir **7 pociones**, se añaden 2 al primer hueco y las 5 restantes ocupan el segundo. La operación devuelve `true`.

Si intentas añadir **8 pociones** a ese mismo inventario, no hay espacio suficiente. La operación devuelve `false` y el inventario conserva su contenido original.

## Comprueba estos casos

- Añadir objetos cuando hay huecos vacíos.
- Completar una pila existente y crear otra.
- Rechazar una operación que no cabe sin modificar el inventario.
- Rechazar una cantidad igual a cero o negativa.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
