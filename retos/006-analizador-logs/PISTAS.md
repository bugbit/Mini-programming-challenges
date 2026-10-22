# Pistas: Analizador de logs

Consulta una pista cada vez. El enunciado no depende de este documento.

## Pista 1

Separa dos responsabilidades: decidir si una línea es válida y extraer sus partes, y actualizar los contadores. Si la validación termina antes de tocar ningún contador, una línea inválida no puede dejar el resumen a medias.

## Pista 2

El formato tiene posiciones casi fijas. Una vez localizado el `]`, sabes exactamente dónde deberían estar el espacio, los dígitos de la hora, los dos puntos, el segundo espacio y el comienzo del mensaje. Antes de mirar cualquiera de esas posiciones, comprueba que la línea es lo bastante larga.

## Pista 3

Valida en este orden: quitar el `\r` final, ignorar las líneas vacías, buscar el nivel entre `[` y `]`, comprobar los separadores, comprobar cada dígito de la hora con `std::isdigit` y, por último, comprobar que el mensaje tiene algo además de espacios. Convierte la hora a número solo después de saber que son dos dígitos.

## Pista 4

`std::string::find` devuelve `std::string::npos` si no encuentra el carácter, y `substr` lanza una excepción si la posición inicial supera la longitud. Para los contadores basta con un `std::map<std::string, int>` por nivel y dos `std::array<int, 24>`: uno para todas las líneas y otro solo para `ERROR`. Para la hora más ocupada, recorre el segundo array de `0` a `23` y quédate con la primera hora que tenga el máximo.
