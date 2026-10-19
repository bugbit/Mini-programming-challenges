using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EventSchedulerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestsScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestsScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    // Registrar un evento válido
    [Test]
    public void TrySchedule_ValidEvent_ReturnsTrue()
    {
        var scheduler = new EventScheduler();
        bool result = scheduler.TrySchedule(
            "Event1",
            5,
            () => { }
        );

        Assert.That(result, Is.True);
    }

    // Registrar dos veces el mismo Id pendiente
    [Test]
    public void TrySchedule_DuplicateId_ReturnsFalse()
    {
        var scheduler = new EventScheduler();
        bool result = scheduler.TrySchedule(
            "Event1",
            5,
            () => { }
        );

        Assert.That(result, Is.True);

        result = scheduler.TrySchedule(
            "Event1",
            5,
            () => { }
        );

        Assert.That(result, Is.False);
    }

    // Registrar prioridad 10
    [Test]
    public void TrySchedule_Priority10_ReturnsFalse()
    {
        var scheduler = new EventScheduler();
        bool result = scheduler.TrySchedule(
            "Event1",
            10,
            () => { }
        );

        Assert.That(result, Is.False);
    }

    // Ejecutar una cola vacía
    [Test]
    public void ExecuteNext_EmptyQueue_ReturnsZero()
    {
        var scheduler = new EventScheduler();
        int result = scheduler.ExecuteNext(1);

        Assert.That(result, Is.EqualTo(0));
    }

    // Ejecutar con maxEvents = 0
    [Test]
    public void ExecuteNext_MaxEventsZero_ReturnsZero()
    {
        var scheduler = new EventScheduler();

        scheduler.TrySchedule(
            "Event1",
            5,
            () => { }
        );

        int result = scheduler.ExecuteNext(0);

        Assert.That(result, Is.EqualTo(0));
    }

    // Cancelar un evento existente
    [Test]
    public void Cancel_ExistingEvent_ReturnsTrue()
    {
        var scheduler = new EventScheduler();
        scheduler.TrySchedule("Event1", 5, () => { });
        bool result = scheduler.Cancel("Event1");
        Assert.That(result, Is.True);
        var executes = scheduler.ExecuteNext(1);
        Assert.That(executes, Is.EqualTo(0));
    }

    // Cancelar un evento inexistente
    [Test]
    public void Cancel_NonExistingEvent_ReturnsFalse()
    {
        var scheduler = new EventScheduler();
        scheduler.TrySchedule("Event2", 5, () => { });
        bool result = scheduler.Cancel("Event1");
        Assert.That(result, Is.False);
    }

    // Dos eventos con igual prioridad
    [Test]
    public void ExecuteNext_EqualPriorityEvents_ExecutesInOrder()
    {
        var scheduler = new EventScheduler();
        var id = 1;
        int id1, id2;

        id1 = id2 = 0;
        scheduler.TrySchedule("Event1", 5, () =>
        {
            id1 = id++;
            Debug.Log("Event1 executed");
        });
        scheduler.TrySchedule("Event2", 5, () =>
        {
            id2 = id++;
            Debug.Log("Event2 executed");
        });
        int result = scheduler.ExecuteNext(2);
        Assert.That(result, Is.EqualTo(2));
        Assert.That(id1, Is.LessThan(id2));
    }

    // Registrar un Id después de ejecutarlo
    [Test]
    public void CanReuseIdAfterExecution()
    {
        var scheduler = new EventScheduler();

        scheduler.TrySchedule("A", 5, () => { });
        scheduler.ExecuteNext(1);

        bool result = scheduler.TrySchedule(
            "A", 5, () => { });

        Assert.That(result, Is.True);
    }
}