# Pistas: Reserva de una sala de reuniones

Consulta una pista cada vez. El enunciado no depende de este documento.

## Pista 1

Antes de pensar en la página, piensa en la sala como una lista de reservas y en tres preguntas: ¿cabe esta reserva en el horario?, ¿choca con alguna existente? y ¿qué horas quedan libres? Si una clase sin interfaz sabe responderlas, la página solo tiene que mostrar las respuestas.

## Pista 2

Dos intervalos que no incluyen su final, `[inicio, fin)`, se solapan cuando cada uno empieza antes de que termine el otro. Comprueba con `10–12` y `12–13` que la condición no los considera un choque. Las horas libres se pueden calcular recorriendo las nueve horas y preguntando si alguna reserva las contiene.

## Pista 3

Haz que la operación de reservar devuelva algo más que `true` o `false`: la página necesita saber el motivo del rechazo y, en el caso de solapamiento, con qué reserva coincide. Valida todo antes de modificar nada; así, cuando una reserva se rechaza, el estado queda intacto sin tener que deshacer cambios.

## Pista 4

Para cancelar, identifica la reserva concreta y no solo su nombre, porque puede haber dos reservas con el mismo. En Blazor, el botón de cada elemento de un `@foreach` puede capturar la propia reserva con `@onclick="() => Cancelar(reserva)"`. En el servicio, comprueba que esa reserva está guardada antes de liberar sus horas.
