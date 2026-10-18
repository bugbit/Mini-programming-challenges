# Reto 2: Sistema de eventos con prioridades

**Tecnología:** aplicación de consola en C# con .NET\
**Tiempo estimado:** 30 minutos

## Enunciado

En un videojuego, varios sistemas pueden reaccionar al mismo evento. Cuando se dispara una alarma, por ejemplo, puedes querer avisar primero a los guardias y después actualizar la interfaz. El orden de registro no siempre coincide con el orden que necesitas.

Implementa un sistema de eventos que permita registrar acciones con una prioridad, eliminar suscripciones y publicar eventos por su nombre. Cada publicación incluye un mensaje de texto que reciben las acciones suscritas.

El sistema debe ofrecer estas operaciones:

- `Subscribe`: recibe el nombre del evento, un identificador de suscripción, una prioridad entera y una acción que recibe el mensaje. Devuelve `true` si registra la suscripción y `false` si ese identificador ya existe para ese evento.
- `Unsubscribe`: recibe el nombre del evento y el identificador de suscripción. Devuelve `true` si elimina la suscripción y `false` si no existe.
- `Publish`: recibe el nombre del evento y el mensaje. Ejecuta las acciones suscritas a ese evento en el orden indicado por las reglas.

## Reglas

- Una prioridad numérica mayor se ejecuta antes. Se admiten prioridades positivas, cero y negativas.
- Si varias suscripciones tienen la misma prioridad, se ejecutan en el orden en que se registraron.
- Los identificadores son únicos dentro de cada evento. El mismo identificador puede usarse en eventos distintos.
- Rechazar un registro duplicado no cambia la acción, la prioridad ni el orden de la suscripción existente.
- Cada publicación ejecuta una vez cada acción suscrita al evento. Las suscripciones se conservan para futuras publicaciones.
- Publicar un evento sin suscripciones no ejecuta ninguna acción ni produce un error.
- Eliminar una suscripción no cambia el orden relativo de las restantes. Si vuelves a registrar el mismo identificador, cuenta como una suscripción nueva.
- Los nombres de evento y los identificadores distinguen mayúsculas y minúsculas.

Para este reto, puedes suponer que los nombres y los identificadores no son nulos ni vacíos, las acciones son válidas y no lanzan excepciones. Las acciones no registran ni eliminan suscripciones ni publican otros eventos. Toda la ejecución es síncrona y ocurre en un solo hilo.

## Ejemplo

Registra estas suscripciones en este orden:

| Evento | Identificador | Prioridad | Acción observable |
|---|---|---|---|
| `Alarma` | `interfaz` | 0 | Muestra `Interfaz: ` seguido del mensaje |
| `Alarma` | `guardias` | 10 | Muestra `Guardias: ` seguido del mensaje |
| `Alarma` | `registro` | 10 | Muestra `Registro: ` seguido del mensaje |
| `PuertaAbierta` | `interfaz` | 20 | Muestra `Puerta: ` seguido del mensaje |

Al publicar `Alarma` con el mensaje `Intruso en el patio`, la salida debe ser:

```text
Guardias: Intruso en el patio
Registro: Intruso en el patio
Interfaz: Intruso en el patio
```

La acción de `PuertaAbierta` no se ejecuta porque pertenece a otro evento.

Si intentas registrar de nuevo `guardias` en `Alarma` con prioridad 100, `Subscribe` devuelve `false`. La publicación siguiente mantiene el mismo orden.

Si eliminas `guardias` de `Alarma`, `Unsubscribe` devuelve `true`. La publicación siguiente ejecuta primero `registro` y después `interfaz`.

Si vuelves a registrar `guardias` con prioridad 10, se ejecuta después de `registro`, porque ambas tienen la misma prioridad y `guardias` acaba de registrarse de nuevo.

## Comprueba estos casos

- Ejecutar varias acciones por prioridad descendente, incluyendo prioridades cero y negativas.
- Mantener el orden de registro cuando las prioridades coinciden.
- Entregar el mismo mensaje a todas las acciones del evento publicado.
- Publicar dos veces el mismo evento y comprobar que cada acción se ejecuta una vez en cada publicación.
- Aislar las suscripciones de eventos distintos, incluso si comparten un identificador.
- Rechazar un registro duplicado sin modificar la suscripción existente.
- Eliminar una suscripción existente y rechazar una segunda eliminación del mismo identificador.
- Volver a registrar una suscripción eliminada y comprobar su nuevo orden entre las de igual prioridad.
- Publicar un evento desconocido o uno cuya última suscripción se haya eliminado.
- Distinguir `Alarma` de `alarma` y `guardias` de `Guardias`.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
