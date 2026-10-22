using ReservaSala.Data;

namespace ReservaSala.Application;

public class SalaServices
{
    private SortedList<int, Reserva> _reservas = new();
    private Dictionary<int, Reserva?> _horas = Enumerable.Range(9, 17 - 9 + 1).ToDictionary(h => h, h => (Reserva?)null);

    public IReadOnlyDictionary<int, Reserva> Reservas { get => _reservas; }
    public IReadOnlyDictionary<int, Reserva?> Horas { get => _horas; }

    public int GetUltimaHoraOcupada(Reserva reserva) => reserva.Hora + reserva.Duracion - 1;

    public int HorasLibres() => Horas.Count(h => h.Value is null);

    public Result<Reserva, string> ValidateReserva(Reserva reserva)
    {
        //  la hora de inicio (de 9 a 17)
        if (reserva.Hora < 9 || reserva.Hora > 17)
            return Result<Reserva, string>.SetError("La Hora de inicio tiene que estar entre las 9 a 17");
        //  duración en horas (de 1 a 3)
        if (reserva.Duracion < 1 || reserva.Duracion > 3)
            return Result<Reserva, string>.SetError("La duracion tiene que ser entre 1 y 3 horas");

        // Una reserva ocupa desde su hora de inicio hasta su hora de fin, sin incluir esta última. Una reserva de 10:00 a 12:00 ocupa las horas 10:00 y 11:00.
        int ultimaHora = GetUltimaHoraOcupada(reserva);

        // Dos reservas pueden ser consecutivas: 10:00–12:00 y 12:00–13:00 son compatibles.
        // Rechaza cualquier reserva que se solape, aunque sea una sola hora, con otra existente.
        for (var hora = reserva.Hora; hora <= ultimaHora; hora++)
            if (Horas.TryGetValue(hora, out var reservaDummy) && reservaDummy != null)
                return Result<Reserva, string>.SetError($"se solapa con {reservaDummy}");

        // Rechaza las reservas que terminen después de las 18:00. Por ejemplo, empezar a las 17:00 con una duración de 2 horas no es válido.
        if (ultimaHora >= 18)
            return Result<Reserva, string>.SetError("termina después de las 18:00");

        // El nombre es obligatorio. Elimina los espacios del principio y del final antes de validarlo y de guardarlo; un nombre formado solo por espacios se rechaza.
        if (string.IsNullOrWhiteSpace(reserva.Nombre))
            return Result<Reserva, string>.SetError("el nombre es obligatorio");

        return reserva;
    }

    public Result<Reserva, string> Reservar(Reserva reserva)
    {
        var result = ValidateReserva(reserva);

        if (!result.Success)
            return result;

        _reservas.Add(reserva.Hora, reserva);

        var ultimaHora = GetUltimaHoraOcupada(reserva);

        for (var hora = reserva.Hora; hora <= ultimaHora; hora++)
            _horas[hora] = reserva;

        return reserva;
    }

    public bool Cancelar(Reserva reserva)
    {
        if (!_reservas.TryGetValue(reserva.Hora, out var reservaDummy))
            return false;

        if (!object.ReferenceEquals(reserva, reservaDummy))
            return false;

        int ultimaHora = GetUltimaHoraOcupada(reserva);

        for (var hora = reserva.Hora; hora <= ultimaHora; hora++)
            _horas[hora] = null;

        return _reservas.Remove(reserva.Hora);
    }
}
