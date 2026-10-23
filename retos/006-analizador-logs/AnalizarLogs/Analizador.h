#pragma once

#include <string>
#include <map>
#include <vector>

enum Niveles { NONE, INFO, WARN, ERROR, DEBUG };

typedef struct _Time
{
	int hora, minuto;
} Time;

class Linea
{
public:
	Niveles nivel;
	Time time;
	std::string mensaje;

	inline Linea() :nivel(NONE), time(), mensaje() {}
};

class Analizador
{
public:
	inline Analizador() :nivelLineasOk(), horaLineasOk(), numLineasNoOk(), horaCountError(), numLineaActual(0) {}
	bool ProcessLine(std::string& linea);
	int GetCount(Niveles level);
	int GetCountByHour(int hour);
	// devuelve los números de las líneas inválidas, en orden creciente.
	inline std::vector<int> GetInvalidLines()
	{
		return numLineasNoOk;
	}
	int GetBusiestErrorHour();
private:
	int numLineaActual;
	std::map<Niveles, int> nivelLineasOk;
	std::map<int, int> horaLineasOk;
	std::vector<int> numLineasNoOk;
	std::map<int, int>horaCountError;

	bool TryNumber(std::string& linea, int& i, int& number);
	bool TryNivel(std::string& linea, int& i, Niveles& nivel);
	bool TryTime(std::string& linea, int& i, Time& time);
	bool TryParseLine(std::string& linea, int& i, Linea& lineaMensaje);
};

