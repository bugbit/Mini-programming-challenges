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

	int hora = 10 * hora1 + hora2;

	if (hora < 0 || hora>23)
		return false;

	int minuto = 10 * minuto1 + minuto2;

	if (minuto < 0 || minuto>60)
		return false;

	time.hora = hora;
	time.minuto = minuto;

	return true;
}

bool Analizador::TryParseLine(std::string& linea, int& i, Linea& lineaMensaje)
{
	if (linea.empty())
		return false;

	if (!TryNivel(linea, i, lineaMensaje.nivel))
		return false;

	if (linea[i] != ' ')
		return false;

	i++;
	if (linea[i] == ' ')
		return false;

	if (!TryTime(linea, i, lineaMensaje.time))
		return false;

	if (linea[i] != ' ')
		return false;

	i++;

	if (linea[i] == ' ')
		return false;

	lineaMensaje.mensaje = linea.substr(i);

	return !lineaMensaje.mensaje.empty();
}

bool Analizador::ProcessLine(std::string& linea)
{
	Linea lineaMensaje;
	int i = 0;
	bool result = TryParseLine(linea, i, lineaMensaje);

	numLineaActual++;
	// nivelLineasOk
	if (result)
	{
		auto it = nivelLineasOk.find(lineaMensaje.nivel);

		if (it != nivelLineasOk.end())
			nivelLineasOk[lineaMensaje.nivel]++;
		else
			nivelLineasOk[lineaMensaje.nivel] = 1;

		// cuántas líneas válidas, de cualquier nivel, tienen esa hora. 
		auto it2 = horaLineasOk.find(lineaMensaje.time.hora);

		if (it2 != horaLineasOk.end())
			horaLineasOk[lineaMensaje.time.hora]++;
		else
			horaLineasOk[lineaMensaje.time.hora] = 1;

		//  hora con más líneas ERROR
		if (lineaMensaje.nivel == ERROR)
		{
			// horaCountError
			auto it3 = horaCountError.find(lineaMensaje.time.hora);

			if (it3 != horaCountError.end())
				horaCountError[lineaMensaje.time.hora]++;
			else
				horaCountError[lineaMensaje.time.hora] = 1;
		}
	}
	else
		numLineasNoOk.push_back(numLineaActual);

	return result;
}

// devuelve cuántas líneas válidas tienen ese nivel. Devuelve 0 para un nivel desconocido.
int Analizador::GetCount(Niveles level)
{
	auto it = nivelLineasOk.find(level);

	return it != nivelLineasOk.end() ? it->second : 0;
}

// devuelve cuántas líneas válidas, de cualquier nivel, tienen esa hora. Devuelve 0 si la hora está fuera de 0–23.
int Analizador::GetCountByHour(int hour)
{
	if (hour < 0 || hour>23)
		return 0;

	auto it = horaLineasOk.find(hour);

	return it != horaLineasOk.end() ? it->second : 0;
}

// devuelve la hora con más líneas ERROR. Si hay empate, devuelve la hora más temprana; si no hay ningún ERROR, devuelve -1
int Analizador::GetBusiestErrorHour()
{
	if (horaCountError.empty())
		return -1;

	int hora = -1;
	int count = 0;

	for (const auto& [horait, countit] : horaCountError)
	{
		if (countit > count || (countit == count && horait < hora))
		{
			count = countit;
			hora = horait;
		}
	}

	return hora;
}