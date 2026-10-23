// AnalizarLogs.cpp : Este archivo contiene la función "main". La ejecución del programa comienza y termina ahí.
//

#include <iostream>

#include "Analizador.h"

const char* lineas[] =
{
	"[INFO] 09:15 Servidor iniciado",
	"[ERROR] 09:47 No se pudo conectar a la base de datos",
	"",
	"[WARN] 10:02 Uso de memoria al 85 %",
	"[ERROR] 10:30 Tiempo de espera agotado",
	"[ERROR] 10:31 Tiempo de espera agotado",
	"[DEBUG] 10:40 Valor de x = 3",
	"[ERROR] 9:50 Reintento fallido",
	"[INFO] 24:00 Cambio de día",
	"[INFO] 11:05",
	"[error] 11:10 Disco lleno",
	"[WARN]  11:20 Doble espacio",
	"[ERROR] 11:59 Fallo en [pagos]: reintentando",
	nullptr
};

int main()
{
	Analizador analizador;


	for (const char** lineasPtr = lineas; *lineasPtr != nullptr; lineasPtr++)
	{
		std::string linea(*lineasPtr);
		bool result = analizador.ProcessLine(linea);

		std::cout << linea << std::endl << "Result: " << result << std::endl;
	}

	std::cout << "GetCount(\"INFO\")" << analizador.GetCount(INFO) << std::endl;
	std::cout << "GetCount(\"WARN\")" << analizador.GetCount(WARN) << std::endl;
	std::cout << "GetCount(\"ERROR\")" << analizador.GetCount(ERROR) << std::endl;
	std::cout << "GetCount(\"DEBUG\")" << analizador.GetCount(DEBUG) << std::endl;
	std::cout << "GetCountByHour(9): " << analizador.GetCountByHour(9) << std::endl;
	std::cout << "GetCountByHour(10): " << analizador.GetCountByHour(10) << std::endl;
	std::cout << "GetCountByHour(11): " << analizador.GetCountByHour(11) << std::endl;
	std::cout << "GetCountByHour(25): " << analizador.GetCountByHour(25) << std::endl;
	std::cout << "GetInvalidLines(): ";

	for (const int nunLine : analizador.GetInvalidLines())
		std::cout << nunLine << ',';
	std::cout << "\r." << std::endl;
	std::cout << "GGetBusiestErrorHour(): " << analizador.GetBusiestErrorHour() << std::endl;
}

// Ejecutar programa: Ctrl + F5 o menú Depurar > Iniciar sin depurar
// Depurar programa: F5 o menú Depurar > Iniciar depuración

// Sugerencias para primeros pasos: 1. Use la ventana del Explorador de soluciones para agregar y administrar archivos
//   2. Use la ventana de Team Explorer para conectar con el control de código fuente
//   3. Use la ventana de salida para ver la salida de compilación y otros mensajes
//   4. Use la ventana Lista de errores para ver los errores
//   5. Vaya a Proyecto > Agregar nuevo elemento para crear nuevos archivos de código, o a Proyecto > Agregar elemento existente para agregar archivos de código existentes al proyecto
//   6. En el futuro, para volver a abrir este proyecto, vaya a Archivo > Abrir > Proyecto y seleccione el archivo .sln
