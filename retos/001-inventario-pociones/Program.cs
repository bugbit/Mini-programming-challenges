Console.WriteLine("Hello, World!");

TestHuecos0();
TestPilaExistenteYCrearOtra();
TestRechazarOperacion();
TestRechazarCantidadInvalida();

// Añadir objetos cuando hay huecos vacíos.
void TestHuecos0()
{
    var inventory = new Inventory(3, 5);

    Console.WriteLine("TestHuecos0");
    inventory.Print();
    Console.WriteLine("Adding 3 Potions to empty inventory:");
    var result = inventory.TryAdd(ItemType.Potion, 3);
    inventory.Print(result);
    Console.WriteLine("Adding 2 Coins to empty inventory:");
    result = inventory.TryAdd(ItemType.Coin, 2);
    inventory.Print(result);
    Console.WriteLine("-----------------------------------------");
}

// Completar una pila existente y crear otra.
void TestPilaExistenteYCrearOtra()
{
    var inventory = new Inventory(3, 5);

    Console.WriteLine("TestPilaExistenteYCrearOtra");
    inventory.TryAdd(ItemType.Potion, 3);
    inventory.Print();
    Console.WriteLine("Adding 3 Potions to empty inventory:");
    var result = inventory.TryAdd(ItemType.Potion, 3);
    inventory.Print(result);
    Console.WriteLine("-----------------------------------------");
}

// Rechazar una operación que no cabe sin modificar el inventario.
void TestRechazarOperacion()
{
    var inventory = new Inventory(3, 5);

    Console.WriteLine("TestRechazarOperacion");
    inventory.TryAdd(ItemType.Potion, 3);
    inventory.Print();
    Console.WriteLine("Adding 20 Potions to empty inventory:");
    var result = inventory.TryAdd(ItemType.Potion, 20);
    inventory.Print(result);
    Console.WriteLine("-----------------------------------------");
}

// Rechazar una cantidad igual a cero o negativa.
void TestRechazarCantidadInvalida()
{
    var inventory = new Inventory(3, 5);

    Console.WriteLine("TestRechazarCantidadInvalida");
    inventory.TryAdd(ItemType.Potion, 0);
    inventory.Print();
    var result = inventory.TryAdd(ItemType.Potion, -5);
    inventory.Print(result);
    Console.WriteLine("-----------------------------------------");
}

public enum ItemType { Potion, Coin, Key }

public class InventoryItem
{
    public ItemType? ItemType { get; set; }
    public int Free { get; set; }
}


public class Inventory
{
    public InventoryItem[] ItemsType { get; }

    public Inventory(int huecos, int unidades)
    {
        ItemsType = Enumerable.Range(0, huecos).Select(_ => new InventoryItem { Free = unidades }).ToArray();
    }

    public bool TryAdd(ItemType item, int count)
    {
        if (count <= 0)
            return false;

        var candidatos = new List<(int idx, int count)>();
        var quedan = count;

        if (quedan > 0)
            CompletarPilasExistentes(candidatos, item, ref quedan);
        if (quedan > 0)
            CompletarHuecosVacios(candidatos, ref quedan);

        if (quedan != 0)
            return false;

        foreach (var (idx, usar) in candidatos)
        {
            var itemType = ItemsType[idx];

            if (!itemType.ItemType.HasValue)
                itemType.ItemType = item;
            itemType.Free -= usar;
        }

        return true;
    }

    public void Print()
    {
        for (int i = 0; i < ItemsType.Length; i++)
        {
            var itemType = ItemsType[i];
            Console.WriteLine($"Slot {i}: {itemType.ItemType?.ToString() ?? "Empty"}, Free: {itemType.Free}");
        }
    }

    public void Print(bool result)
    {
        Console.WriteLine($"Result: {result}");
        Print();
    }

    // Completa primero las pilas existentes del mismo tipo.
    private void CompletarPilasExistentes(List<(int idx, int count)> candidatos, ItemType item, ref int quedan)
    {
        for (int i = 0; i < ItemsType.Length && quedan > 0; i++)
        {
            var itemType = ItemsType[i];

            if (itemType.ItemType.HasValue && itemType.ItemType == item)
            {
                var usar = Math.Min(itemType.Free, quedan);

                if (usar > 0)
                {
                    quedan -= usar;
                    candidatos.Add((i, usar));
                }
            }
        }
    }

    // Si todavía quedan unidades, usa los huecos vacíos.
    private void CompletarHuecosVacios(List<(int idx, int count)> candidatos, ref int quedan)
    {
        for (int i = 0; i < ItemsType.Length && quedan > 0; i++)
        {
            var itemType = ItemsType[i];

            if (!itemType.ItemType.HasValue)
            {
                var usar = Math.Min(itemType.Free, quedan);

                if (usar > 0)
                {
                    quedan -= usar;
                    candidatos.Add((i, usar));
                }
            }
        }
    }
}