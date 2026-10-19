using System;
using System.Collections.Generic;
using UnityEngine;

public class Event : IComparable<Event>
{
    public string Id { get; set; }
    public int Priority { get; set; }
    public int Orden { get; set; }
    public Action Action { get; set; }
    public bool Ejecuted { get; set; } = false;

    public int CompareTo(Event other)
    {
        var cmp = Ejecuted.CompareTo(other.Ejecuted);

        if (cmp != 0)
            return cmp; // Los no ejecutados primero

        cmp = other.Priority.CompareTo(Priority);

        return (cmp != 0)
            ? cmp // Mayor prioridad primero
            : Orden.CompareTo(other.Orden); // FIFO para la misma prioridadd
    }
}

public class EventScheduler
{
    private int _lastOrden = 0;
    private readonly List<Event> _events = new();
    private readonly Dictionary<string, Event> _eventIds = new();

    private readonly Event _eventFirstExecuted =
        new() { Priority = int.MaxValue, Orden = int.MinValue, Ejecuted = true };

    public bool PrintLogs { get; set; } = true;
    
    public int Count=> _events.Count;

    /*
Reglas:
- No puede haber dos eventos pendientes con el mismo Id.
- La prioridad debe estar comprendida entre 0 y 9.
- Action no puede ser null.
- Si el evento es válido, se añade a la cola y devuelve true.
- Si incumple alguna regla, devuelve false sin modificar los eventos pendientes.
     */
    public bool TrySchedule(string id, int priority, Action action)
    {
        if (priority < 0 || priority > 9)
            return false;
        if (action == null)
            return false;
        var @event = new Event
            { Id = id, Priority = priority, Orden = _lastOrden++, Action = action, Ejecuted = false };

        if (!_eventIds.TryAdd(id, @event))
            return false;

        var idx = _events.BinarySearch(@event);

        if (idx < 0)
            idx = ~idx;

        _events.Insert(idx, @event);

        if (PrintLogs)
        {
            Debug.Log($"Evento {id} programado con prioridad {priority} y orden {@event.Orden}");
            PrintEvents();
        }

        return true;
    }

    /*
Reglas de ejecución:
1. Los eventos de mayor prioridad se ejecutan primero.
2. Si dos eventos tienen la misma prioridad, se ejecutan en el orden en que fueron registrados (FIFO).
3. Cada evento se elimina de los pendientes al seleccionarlo para ejecutarlo.
4. Si maxEvents es 0 o negativo, no se ejecuta ninguno.
5. El método devuelve el número de eventos ejecutados.
Para este reto, puedes asumir que las acciones no lanzan excepciones y que no se registran eventos nuevos durante ExecuteNext.
     */
    public int ExecuteNext(int maxEvents)
    {
        if (maxEvents <= 0)
            return 0;

        if (PrintLogs)
            Debug.Log($"Ejecutando hasta {maxEvents} eventos...");

        int executedCount = 0;
        var idx = 0;

        while (idx < _events.Count && executedCount < maxEvents)
        {
            var @event = _events[idx];

            if (@event.Ejecuted)
                break;

            @event.Action.Invoke();
            @event.Ejecuted = true;
            executedCount++;
            _events.RemoveAt(idx);

            var idxF = _events.BinarySearch(@event);

            if (idxF < 0)
                idxF = ~idxF;

            _events.Insert(idxF, @event);

            if (PrintLogs)
                PrintEvents();
        }

        if (PrintLogs)
            Debug.Log($"Ejecutado {executedCount} eventos...");

        return executedCount;
    }

    /*
Elimina el evento pendiente con el identificador indicado.
Devuelve true si lo encuentra y elimina.
Devuelve false si no existe.
Un evento ya ejecutado no puede cancelarse.
     */
    public bool Cancel(string id)
    {
        if (!_eventIds.TryGetValue(id, out var @event))
            return false;

        if (@event.Ejecuted)
            return false;

        Debug.Log($"Evento {id} programado con cancelado");

        return _eventIds.Remove(id) && _events.Remove(@event);
    }

    public void PrintEvents()
    {
        foreach (var @event in _events)
            Debug.Log(
                $"Id: {@event.Id}, Priority: {@event.Priority}, Orden: {@event.Orden}, Ejecuted: {@event.Ejecuted}");
    }
}