# Reto 6: Analizador de logs

**Tecnología:** aplicación de consola en C++17\
**Tiempo estimado:** 30 minutos

## Enunciado

Un servidor escribe un fichero de log con una línea por evento. Antes de investigar un fallo, quieres un resumen rápido: cuántos eventos hay de cada nivel, en qué horas se concentran y qué líneas no respetan el formato.

Implementa un analizador que procese las líneas de una en una, en el orden en que aparecen en el fichero. Cada línea válida tiene este formato:

```text
[NIVEL] HH:MM Mensaje
```

El analizador debe ofrecer estas operaciones:

- `ProcessLine(line)`: procesa la siguiente línea. Devuelve `true` si la línea es válida y se ha contabilizado, y `false` en cualquier otro caso.
- `GetCount(level)`: devuelve cuántas líneas válidas tienen ese nivel. Devuelve `0` para un nivel desconocido.
- `GetCountByHour(hour)`: devuelve cuántas líneas válidas, de cualquier nivel, tienen esa hora. Devuelve `0` si la hora está fuera de `0`–`23`.
- `GetInvalidLines()`: devuelve los números de las líneas inválidas, en orden creciente.
- `GetBusiestErrorHour()`: devuelve la hora con más líneas `ERROR`. Si hay empate, devuelve la hora más temprana; si no hay ningún `ERROR`, devuelve `-1`.

Puedes elegir las firmas concretas y los tipos. Las líneas pueden venir de un fichero de texto o de un `std::vector<std::string>` definido en `main`; lo importante es que el analizador reciba una línea cada vez. No necesitas un menú interactivo: basta con demostrar las operaciones mediante llamadas desde `main`.

## Reglas

- Los niveles válidos son exactamente `INFO`, `WARN` y `ERROR`, en mayúsculas. `[error]` o `[DEBUG]` hacen que la línea sea inválida.
- La línea empieza directamente por `[`, sin espacios delante.
- Después de `]` hay exactamente un espacio, la hora, exactamente un espacio y el mensaje.
- La hora usa siempre dos dígitos para las horas y dos para los minutos, separados por `:`. Las horas van de `00` a `23` y los minutos de `00` a `59`. `9:50`, `24:00` y `10:60` son inválidas.
- El mensaje debe contener al menos un carácter que no sea un espacio. Puede contener cualquier texto, incluidos espacios, corchetes y dos puntos.
- Si una línea termina en `\r`, como ocurre con los ficheros guardados en Windows, ignora ese carácter antes de validarla.
- Las líneas vacías, o formadas solo por espacios, se ignoran: `ProcessLine` devuelve `false`, pero no cuentan como inválidas.
- Los números de línea empiezan en `1` y avanzan con cada llamada a `ProcessLine`, incluidas las líneas vacías, para que coincidan con las del fichero.
- Una línea inválida no modifica ningún contador; solo se añade su número a la lista de líneas inválidas.
- Ninguna línea, por corta o extraña que sea (`[`, `[INFO]`, `]`), puede provocar un acceso fuera de rango ni detener el programa.

Puedes suponer que el texto es ASCII, que las líneas no contienen saltos de línea internos y que todas las llamadas ocurren en un solo hilo. No necesitas fechas, zonas horarias, otros formatos de log ni guardar el resumen en disco.

## Ejemplo

Procesa estas líneas en orden:

| N.º | Línea | `ProcessLine` | Motivo |
|---|---|---|---|
| 1 | `[INFO] 09:15 Servidor iniciado` | `true` | |
| 2 | `[ERROR] 09:47 No se pudo conectar a la base de datos` | `true` | |
| 3 | *(vacía)* | `false` | Se ignora |
| 4 | `[WARN] 10:02 Uso de memoria al 85 %` | `true` | |
| 5 | `[ERROR] 10:30 Tiempo de espera agotado` | `true` | |
| 6 | `[ERROR] 10:31 Tiempo de espera agotado` | `true` | |
| 7 | `[DEBUG] 10:40 Valor de x = 3` | `false` | Nivel desconocido |
| 8 | `[ERROR] 9:50 Reintento fallido` | `false` | Hora con un solo dígito |
| 9 | `[INFO] 24:00 Cambio de día` | `false` | Hora fuera de rango |
| 10 | `[INFO] 11:05` | `false` | Sin mensaje |
| 11 | `[error] 11:10 Disco lleno` | `false` | Nivel en minúsculas |
| 12 | `[WARN]  11:20 Doble espacio` | `false` | Dos espacios después de `]` |
| 13 | `[ERROR] 11:59 Fallo en [pagos]: reintentando` | `true` | |

Después de procesarlas:

| Consulta | Resultado |
|---|---|
| `GetCount("INFO")` | `1` |
| `GetCount("WARN")` | `1` |
| `GetCount("ERROR")` | `4` |
| `GetCount("DEBUG")` | `0` |
| `GetCountByHour(9)` | `2` |
| `GetCountByHour(10)` | `3` |
| `GetCountByHour(11)` | `1` |
| `GetCountByHour(25)` | `0` |
| `GetInvalidLines()` | `7, 8, 9, 10, 11, 12` |
| `GetBusiestErrorHour()` | `10` |

La línea 3 no aparece entre las inválidas, pero sí cuenta para numerar las siguientes. La hora `10` tiene dos errores y las horas `9` y `11`, uno cada una.

## Comprueba estos casos

- Procesar una línea válida de cada nivel.
- Aceptar las horas límite `00:00` y `23:59`, y rechazar `24:00` y `10:60`.
- Rechazar horas con un dígito, con letras (`1a:30`) o sin los dos puntos (`1030`).
- Rechazar niveles desconocidos, en minúsculas o sin cerrar el corchete (`[INFO 10:00 Mensaje`).
- Rechazar líneas con espacios de más o de menos, incluido un espacio delante de `[`.
- Rechazar una línea sin mensaje y otra cuyo mensaje solo tenga espacios.
- Aceptar un mensaje que contenga corchetes, dos puntos y espacios.
- Procesar una línea terminada en `\r` y comprobar que se valida igual que sin él.
- Procesar líneas muy cortas (`[`, `]`, `[INFO]`, `[INFO] 10:0`) sin que el programa falle.
- Intercalar líneas vacías y comprobar que la numeración de las inválidas coincide con la del fichero.
- Consultar `GetBusiestErrorHour()` sin errores, con un único error y con un empate entre dos horas.
- Comprobar que, después de una línea inválida, los contadores siguen igual.

Cuando tengas una solución, comparte el código y explica brevemente cómo pensaste el problema antes de implementarlo.
