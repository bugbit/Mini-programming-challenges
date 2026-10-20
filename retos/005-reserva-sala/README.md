# Reto 5: Reserva de una sala de reuniones

**Tecnología:** Blazor (Server o WebAssembly, a tu elección) con .NET 10\
**Tiempo estimado:** 30 minutos

## Enunciado

Una pequeña oficina tiene una única sala de reuniones y quiere una página web sencilla para reservarla durante la jornada de hoy. La sala se puede usar de `9:00` a `18:00`, en bloques de horas completas.

Crea una aplicación Blazor con una página interactiva que permita reservar la sala, ver las reservas del día y cancelarlas. Separa la lógica de las reservas en una clase de C# independiente del componente, de forma que la página solo muestre datos y reaccione a los eventos del usuario.

La página debe ofrecer:

- Un formulario con el nombre de quien reserva, la hora de inicio (de `9` a `17`) y la duración en horas (de `1` a `3`), y un botón **Reservar**.
- Un mensaje que indique si la última reserva se ha aceptado o el motivo por el que se ha rechazado.
- La lista de reservas del día, ordenada por hora de inicio. Cada reserva muestra el nombre, el intervalo (por ejemplo, `10:00–12:00`) y un botón **Cancelar**.
- Una vista de las nueve horas de la jornada (`9:00`, `10:00`… `17:00`) que indique si cada hora está libre u ocupada, junto con el número total de horas libres.

Puedes elegir los nombres de las clases y los métodos, el tipo de proyecto Blazor, los controles del formulario y el diseño visual. Los datos se guardan en memoria: no necesitas base de datos, autenticación ni varios días.

## Reglas

- Una reserva ocupa desde su hora de inicio hasta su hora de fin, sin incluir esta última. Una reserva de `10:00` a `12:00` ocupa las horas `10:00` y `11:00`.
- Dos reservas pueden ser consecutivas: `10:00–12:00` y `12:00–13:00` son compatibles.
- Rechaza cualquier reserva que se solape, aunque sea una sola hora, con otra existente.
- Rechaza las reservas que terminen después de las `18:00`. Por ejemplo, empezar a las `17:00` con una duración de `2` horas no es válido.
- El nombre es obligatorio. Elimina los espacios del principio y del final antes de validarlo y de guardarlo; un nombre formado solo por espacios se rechaza.
- Cuando se rechaza una reserva, la lista y la vista de horas no cambian, y el formulario conserva los valores escritos para que el usuario pueda corregirlos.
- Cuando se acepta una reserva, se vacía el campo del nombre y la lista y la vista de horas se actualizan sin recargar la página.
- El mensaje de rechazo distingue al menos estos motivos: nombre vacío, fuera del horario y solapamiento. En el caso de solapamiento, indica con qué reserva coincide.
- Cancelar una reserva la elimina y libera sus horas inmediatamente. Las demás reservas no cambian.
- Una misma persona puede tener varias reservas en el día. Dos reservas pueden tener el mismo nombre; cancelar una no debe eliminar la otra.
- La lógica de reservas debe poder usarse sin la interfaz: el componente no decide por sí mismo si hay solapamiento ni si una reserva cabe en el horario.

Puedes suponer que un solo usuario usa la página a la vez y que las reservas se pierden al reiniciar la aplicación. No necesitas confirmación antes de cancelar ni editar reservas existentes.

## Ejemplo

Partiendo de una sala sin reservas, ejecuta estas acciones en orden:

| Acción | Resultado | Reservas posteriores | Horas libres |
|---|---|---|---|
| Ana, `10`, `2` h | Aceptada | Ana `10:00–12:00` | 7 |
| Luis, `11`, `1` h | Rechazada: se solapa con Ana `10:00–12:00` | Ana `10:00–12:00` | 7 |
| Luis, `12`, `1` h | Aceptada | Ana `10:00–12:00`, Luis `12:00–13:00` | 6 |
| Marta, `17`, `2` h | Rechazada: termina después de las `18:00` | Ana `10:00–12:00`, Luis `12:00–13:00` | 6 |
| `"   "`, `9`, `1` h | Rechazada: el nombre es obligatorio | Ana `10:00–12:00`, Luis `12:00–13:00` | 6 |
| Marta, `9`, `1` h | Aceptada | Marta `9:00–10:00`, Ana `10:00–12:00`, Luis `12:00–13:00` | 5 |
| Cancelar Ana | — | Marta `9:00–10:00`, Luis `12:00–13:00` | 7 |
| Luis, `11`, `1` h | Aceptada | Marta `9:00–10:00`, Luis `11:00–12:00`, Luis `12:00–13:00` | 6 |

Tras la cancelación, la hora que antes ocupaba Ana queda libre y Luis puede reservarla. Luis tiene ahora dos reservas consecutivas e independientes.

## Comprueba estos casos

- Reservar con la sala vacía y ver la reserva en la lista y en la vista de horas.
- Reservar la primera hora (`9:00`) y una reserva que termine exactamente a las `18:00`.
- Rechazar una reserva que termine después de las `18:00`.
- Aceptar dos reservas consecutivas y rechazar una que se solape solo una hora por el principio, otra por el final y otra que contenga por completo a una existente.
- Rechazar un nombre vacío y uno formado solo por espacios; comprobar que un nombre con espacios alrededor se guarda sin ellos.
- Comprobar que, tras un rechazo, el formulario conserva los valores y la lista no cambia.
- Añadir reservas en desorden y comprobar que la lista aparece ordenada por hora de inicio.
- Cancelar una reserva, comprobar que sus horas quedan libres y volver a reservarlas.
- Crear dos reservas con el mismo nombre y cancelar solo una.
- Llenar la jornada completa, comprobar que hay `0` horas libres y que cualquier nueva reserva se rechaza.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
