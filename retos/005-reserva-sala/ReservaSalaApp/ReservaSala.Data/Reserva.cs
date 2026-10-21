namespace ReservaSala.Data;

public class Reserva
{
    public string? Nombre { get; init; }
    public required int Hora { get; init; }
    public required int Duracion { get; init; }

    public override string ToString() => $"{Nombre} {Hora}:00-{Hora + Duracion}:00";
}
