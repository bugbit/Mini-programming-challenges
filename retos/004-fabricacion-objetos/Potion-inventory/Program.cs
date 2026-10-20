
using System.Diagnostics;
using Microsoft.VisualBasic;

void Sample()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    // comienza con 7 unidades de Hierro, 4 de Madera y una Espada
    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 7);
    inventary.Items.Add("Madera", 4);
    inventary.Items.Add("Espada", 1);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Console.WriteLine("TryCraft(recetaEspada)");
    Console.WriteLine($"Result:{result}");
    inventary.Print();

    result = craft.TryCraft(recetaEspada);
    Console.WriteLine("TryCraft(recetaEspada)");
    Console.WriteLine($"Result:{result}");
    inventary.Print();

    result = craft.TryCraft(recetaEspada);
    Console.WriteLine("TryCraft(recetaEspada)");
    Console.WriteLine($"Result:{result}");
    inventary.Print();

    Console.WriteLine("Piedra: {0}", inventary.GetQuantity("Piedra"));
}

// Fabricar con las cantidades exactas: los materiales quedan a cero y aparece una unidad del objeto producido.
void Test1()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 3);
    inventary.Items.Add("Madera", 2);
    inventary.Items.Add("Espada", 0);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 0, "Hierro==0");
    Debug.Assert(inventary.GetQuantity("Madera") == 0, "Madera==0");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
}

// Fabricar con materiales sobrantes y comprobar las cantidades restantes.
void Test2()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 7);
    inventary.Items.Add("Madera", 4);
    inventary.Items.Add("Espada", 1);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 4, "Hierro==4");
    Debug.Assert(inventary.GetQuantity("Madera") == 2, "Madera==2");
    Debug.Assert(inventary.GetQuantity("Espada") == 2, "Espada==2");
}

// Fabricar un objeto que ya existe y comprobar que su cantidad aumenta en uno.
void Test3()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 7);
    inventary.Items.Add("Madera", 4);
    inventary.Items.Add("Espada", 1);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Espada") == 2, "Espada==2");
}

// Rechazar un intento cuando un material tiene cantidad insuficiente, aunque los demás alcancen.
void Test4()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 3);
    inventary.Items.Add("Madera", 1);
    inventary.Items.Add("Espada", 1);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(!result, "TryCraft(recetaEspada) == false");
    Debug.Assert(inventary.GetQuantity("Hierro") == 3, "Hierro==3");
    Debug.Assert(inventary.GetQuantity("Madera") == 1, "Madera==1");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
}

// Rechazar un intento cuando un material no existe. Comprueba que se conserva todo el inventario, incluido cualquier material suficiente que figure antes en la receta.
void Test5()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 7);
    inventary.Items.Add("Madera", 0);
    inventary.Items.Add("Espada", 1);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(!result, "TryCraft(recetaEspada) == false");
    Debug.Assert(inventary.GetQuantity("Hierro") == 7, "Hierro==7");
    Debug.Assert(inventary.GetQuantity("Madera") == 0, "Madera==0");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
}

// Intentar fabricar con el inventario vacío.
void Test6()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(!result, "TryCraft(recetaEspada) == false");
    Debug.Assert(inventary.GetQuantity("Hierro") == 0, "Hierro==0");
    Debug.Assert(inventary.GetQuantity("Madera") == 0, "Madera==0");
    Debug.Assert(inventary.GetQuantity("Espada") == 0, "Espada==0");
}

// Repetir la fabricación hasta agotar los recursos y comprobar que el siguiente intento no cambia nada.
void Test7()
{
    // La receta de Espada necesita 3 unidades de Hierro y 2 de Madera, y produce una unidad de Espada.    
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 6);
    inventary.Items.Add("Madera", 4);
    inventary.Items.Add("Espada", 0);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 3, "Hierro==2");
    Debug.Assert(inventary.GetQuantity("Madera") == 2, "Madera==2");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");

    result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 0, "Hierro==0");
    Debug.Assert(inventary.GetQuantity("Madera") == 0, "Madera==0");
    Debug.Assert(inventary.GetQuantity("Espada") == 2, "Espada==2");

    result = craft.TryCraft(recetaEspada);
    Debug.Assert(!result, "TryCraft(recetaEspada) == false");
    Debug.Assert(inventary.GetQuantity("Hierro") == 0, "Hierro==0");
    Debug.Assert(inventary.GetQuantity("Madera") == 0, "Madera==0");
    Debug.Assert(inventary.GetQuantity("Espada") == 2, "Espada==2");
}

// Usar dos recetas que compartan un material y comprobar que ambas utilizan las cantidades actuales del mismo inventario.
void Test8()
{
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var recetaSuperEspada = new Recipient { ItemNameProduce = "SuperEspada", Quantityproduces = 1 };

    recetaSuperEspada.Items.Add(new RecipientItem { ItemName = "Acero", Quantity = 5 });
    recetaSuperEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 3 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 10);
    inventary.Items.Add("Madera", 10);
    inventary.Items.Add("Acero", 10);
    inventary.Items.Add("Espada", 0);
    inventary.Items.Add("SuperEspada", 0);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 7, "Hierro==7");
    Debug.Assert(inventary.GetQuantity("Acero") == 10, "Acero==10");
    Debug.Assert(inventary.GetQuantity("Madera") == 8, "Madera==8");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
    Debug.Assert(inventary.GetQuantity("SuperEspada") == 0, "Espada==0");

    result = craft.TryCraft(recetaSuperEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 7, "Hierro==7");
    Debug.Assert(inventary.GetQuantity("Acero") == 5, "Acero==5");
    Debug.Assert(inventary.GetQuantity("Madera") == 5, "Madera==5");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
    Debug.Assert(inventary.GetQuantity("SuperEspada") == 1, "Espada==1");
}

// Comprobar que los objetos ajenos a la receta y las propias recetas no cambian.
void Test9()
{
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 10);
    inventary.Items.Add("Madera", 10);
    inventary.Items.Add("Acero", 10);
    inventary.Items.Add("Espada", 0);
    inventary.Items.Add("SuperEspada", 0);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 7, "Hierro==7");
    Debug.Assert(inventary.GetQuantity("Acero") == 10, "Acero==10");
    Debug.Assert(inventary.GetQuantity("Madera") == 8, "Madera==8");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
    Debug.Assert(inventary.GetQuantity("SuperEspada") == 0, "Espada==0");
}

// Consultar un objeto ausente y distinguir nombres como Hierro y hierro.
void Test10()
{
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 10);
    inventary.Items.Add("hierro", 20);
    inventary.Items.Add("Madera", 10);
    inventary.Items.Add("Acero", 10);
    inventary.Items.Add("Espada", 0);
    inventary.Items.Add("SuperEspada", 0);

    Debug.Assert(inventary.GetQuantity("Hierro") == 10, "Hierro==10");
    Debug.Assert(inventary.GetQuantity("hierro") == 20, "hierro==20");
}

// La comprobación de materiales funciona, pero hay un error al fabricar un objeto que todavía no existe en el inventario.
void Test11()
{
    var recetaEspada = new Recipient { ItemNameProduce = "Espada", Quantityproduces = 1 };

    recetaEspada.Items.Add(new RecipientItem { ItemName = "Hierro", Quantity = 3 });
    recetaEspada.Items.Add(new RecipientItem { ItemName = "Madera", Quantity = 2 });

    var inventary = new Inventory();

    inventary.Items.Add("Hierro", 10);
    inventary.Items.Add("Madera", 10);
    inventary.Items.Add("Acero", 10);

    var craft = new Craft { Inventory = inventary };

    var result = craft.TryCraft(recetaEspada);
    Debug.Assert(result, "TryCraft(recetaEspada) == true");
    Debug.Assert(inventary.GetQuantity("Hierro") == 7, "Hierro==7");
    Debug.Assert(inventary.GetQuantity("Acero") == 10, "Acero==10");
    Debug.Assert(inventary.GetQuantity("Madera") == 8, "Madera==8");
    Debug.Assert(inventary.GetQuantity("Espada") == 1, "Espada==1");
}

Sample();
Test1();
Test2();
Test3();
Test4();
Test5();
Test6();
Test7();
Test8();
Test9();
Test10();
Test11();

public class RecipientItem
{
    public required string ItemName { get; init; }
    public required int Quantity { get; init; }
}

public class Recipient
{
    public required string ItemNameProduce { get; init; }
    public required int Quantityproduces { get; init; }
    public List<RecipientItem> Items { get; } = new();
}

public class Inventory
{
    public Dictionary<string, int> Items { get; } = new();

    public int GetQuantity(string itemName) => Items.GetValueOrDefault(itemName, 0);
    public bool TryCatch(string itemName, int quantity)
    {
        var itemQuantity = GetQuantity(itemName);

        if (quantity > itemQuantity)
            return false;

        Items[itemName] -= quantity;

        return true;
    }

    public void Produce(string itemName, int quantity)
    {
        if (!Items.TryGetValue(itemName, out var quantityItem))
            Items.Add(itemName, quantity);
        else
            Items[itemName] = quantityItem + quantity;
    }

    public void Print()
    {
        foreach (var item in Items)
            Console.WriteLine($"{item.Key} : {item.Value}");
    }
}

public class Craft
{
    public required Inventory Inventory { get; init; }

    // intenta fabricar una unidad del objeto indicado en la receta. Devuelve true si completa la fabricación y false si faltan materiales.
    public bool TryCraft(Recipient recipient)
    {
        var items =
        (
            from i in recipient.Items
            group i by i.ItemName into g
            select new { itemName = g.Key, quantity = g.Sum(gg => gg.Quantity) }
        ).ToArray();

        if (items.Any(i => i.quantity > Inventory.GetQuantity(i.itemName)))
            return false;

        foreach (var item in items)
            Inventory.TryCatch(item.itemName, item.quantity);

        Inventory.Produce(recipient.ItemNameProduce, recipient.Quantityproduces);

        return true;
    }
}