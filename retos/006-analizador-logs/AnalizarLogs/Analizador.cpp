#include "Analizador.h"

bool Analizador::TryNumber(std::string& linea, int& i, int& number)
{
	char ch = linea[i];

	if (!isdigit(ch))
		return false;

	number = (int)(ch - '0');
	i++;

	return true;
}

bool Analizador::TryNivel(std::string& linea, int& i, Niveles& nivel)
{
	if (!linea.starts_with('['))
		return false;

	bool tagClosed = false;
	int j;

	for (j = 1; j < linea.length(); j++)
	{
		char ch = linea[j];

		if (ch == ']')
		{
			tagClosed = true;
			break;
		}
	}

	if (!tagClosed)
		return false;

	std::string levelStr = linea.substr(1, j - 1);

	i = j;

	if (levelStr == "INFO")
		nivel = INFO;
	else if (levelStr == "WARN")
		nivel = WARN;
	else if (levelStr == "ERROR")
		nivel = ERROR;
	else
		return false;

	i++;

	return true;
}

bool Analizador::TryTime(std::string& linea, int& i, Time& time)
{
	int hora1, hora2, minuto1, minuto2;

	if (!TryNumber(linea, i, hora1))
		return false;
	if (!TryNumber(linea, i, hora2))
		return false;
	if (linea[i] != ':')
		return false;
	i++;
	if (!TryNumber(linea, i, minuto1))
		return false;
	if (!TryNumber(linea, i, minuto2))
		return false;

	time.hora = 10 * hora1 + hora2;
	time.minuto = 10 * minuto1 + minuto2;

	return true;
}

bool Analizador::TryParseLine(std::string& linea, int& i, Linea& lineaMensaje)
{
	if (!TryNivel(linea, i, lineaMensaje.nivel))
		return false;

	if (linea[i] != ' ')
		return false;

	i++;
	if (!TryTime(linea, i, lineaMensaje.time))
		return false;

	if (linea[i] != ' ')
		return false;

	i++;
	lineaMensaje.mensaje = linea.substr(i);

	return true;
}

bool Analizador::ProcessLine(std::string& linea)
{
	Linea lineaMensaje;
	int i = 0;
	bool result = TryParseLine(linea, i, lineaMensaje);

	// nivelLineasOk
	if (result)
	{
		auto it = nivelLineasOk.find(lineaMensaje.nivel);

		if (it != nivelLineasOk.end())
			nivelLineasOk[lineaMensaje.nivel]++;
		else
			nivelLineasOk[lineaMensaje.nivel] = 1;
	}

	return result;
}

// devuelve cuántas líneas válidas tienen ese nivel. Devuelve 0 para un nivel desconocido.
int Analizador::GetCount(Niveles level)
{
	auto it = nivelLineasOk.find(level);

	return it != nivelLineasOk.end() ? it->second : 0;
}