using ReservaSala.Data;
using System.ComponentModel.DataAnnotations;

namespace ReservaSalaApp.ViewModels;

public class AltaReservaViewModel
{
    public string? Nombre { get; set; }
    [Required] public int? Hora { get; set; }
    [Required] public int? Duracion { get; set; }

    public Reserva ToReserva() => new()
    {
        Nombre = Nombre?.Trim() ,
        Hora = Hora ?? 0,
        Duracion = Duracion ?? 0
    };

    public void CleanData()
    {
        Nombre = null;
    }

}
