#include <cassert>
#include <iostream>
#include <ostream>
#include <stack>
#include <string>

typedef struct __Position__
{
    int x;
    int y;
} Position;

/*
Las coordenadas válidas van de 0 a 4, ambos incluidos, en cada eje.
Un movimiento solo es válido si avanza una casilla en una dirección: (1, 0), (-1, 0), (0, 1) o (0, -1).
Rechaza los movimientos diagonales, los desplazamientos de más de una casilla, (0, 0) y cualquier movimiento que salga del tablero.
Cada movimiento aceptado se puede deshacer una vez mientras siga aplicado. Deshacer varios movimientos recorre el historial desde el más reciente.
Rehacer recupera los movimientos en el orden necesario para reconstruir el recorrido anterior. Por ejemplo, si deshaces primero B y después A, debes rehacer primero A y después B.
Aceptar un movimiento nuevo después de deshacer descarta todos los movimientos que quedaban disponibles para rehacer.
Un movimiento rechazado conserva la posición y todo el historial, incluidos los movimientos disponibles para rehacer.
Si Undo o Redo devuelven false, el estado no cambia.
Deshacer y rehacer no cuentan como movimientos nuevos ni deben duplicar entradas en el historial.
 */
class MovementUndoReUndo
{
public:
    inline MovementUndoReUndo(const int w, const int h) : width(w), height(h), undoStack(), reUndoStack()
    {
        currentPosition.x = 0;
        currentPosition.y = 0;
    }

    bool Move(int dx, int dy);
    bool Undo();
    bool Redo();
    void Print();

    inline void GetPosition(Position& pos) const
    {
        pos = currentPosition;
    }

private:
    int width, height;
    Position currentPosition;
    std::stack<Position> undoStack;
    std::stack<Position> reUndoStack;
};

// intenta desplazar al personaje. Devuelve true si acepta el movimiento y false si lo rechaza.
bool MovementUndoReUndo::Move(int dx, int dy)
{
    // Las coordenadas válidas van de 0 a 4, ambos incluidos, en cada eje.
    if (currentPosition.x + dx < 0 || currentPosition.x + dx >= width ||
        currentPosition.y + dy < 0 || currentPosition.y + dy >= height)
        return false;

    // Un movimiento solo es válido si avanza una casilla en una dirección: (1, 0), (-1, 0), (0, 1) o (0, -1).
    // Rechaza los movimientos diagonales, los desplazamientos de más de una casilla, (0, 0) y cualquier movimiento que salga del tablero.
    if (!((dx == 1 && dy == 0) || (dx == -1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 0 && dy == -1)))
        return false;

    undoStack.push(currentPosition);
    currentPosition.x += dx;
    currentPosition.y += dy;

    return true;
}

// deshace el último movimiento que siga aplicado. Devuelve true si puede deshacer y false si no hay ninguno.
bool MovementUndoReUndo::Undo()
{
    if (undoStack.empty())
        return false;

    reUndoStack.push(currentPosition);
    currentPosition = undoStack.top();
    undoStack.pop();

    return true;
}

// vuelve a aplicar el último movimiento deshecho que todavía pueda recuperarse. Devuelve true si puede rehacer y false si no hay ninguno.
bool MovementUndoReUndo::Redo()
{
    if (reUndoStack.empty())
        return false;

    undoStack.push(currentPosition);
    currentPosition = reUndoStack.top();
    reUndoStack.pop();

    return true;
}

void MovementUndoReUndo::Print()
{
    std::string w(2 * width + 1, '-');

    std::cout << "current position (" << currentPosition.x << ", " << currentPosition.y << ")\n";
    for (int y = 0; y < height; y++)
    {
        std::cout << w << std::endl;
        for (int x = 0; x < width; x++)
        {
            std::cout << '|';
            if (x == currentPosition.x && y == currentPosition.y)
                std::cout << '*';
            else
                std::cout << '.';
        }
        std::cout << '|' << std::endl;
    }
    std::cout << w << std::endl;
}

void Samples()
{
    MovementUndoReUndo movement_undo_re_undo(5, 5);
    bool result;

    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Move(1, 0);
    std::cout << "Move(1, 0)\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Move(0, 1);
    std::cout << "Move(0, 1)\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Move(1, 0);
    std::cout << "Move(1, 0)\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Undo();
    std::cout << "Undo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Undo();
    std::cout << "Undo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Redo();
    std::cout << "Redo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Move(1, 1);
    std::cout << "Move(1, 1)\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Redo();
    std::cout << "Redo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Undo();
    std::cout << "Undo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Move(-1, 0);
    std::cout << "Move(-1, 0)\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
    result = movement_undo_re_undo.Redo();
    std::cout << "Redo()\nResult: " << result << std::endl;
    movement_undo_re_undo.Print();
}

// Consultar la posición inicial y llamar a Undo y Redo sin movimientos previos.
void Test1()
{
    MovementUndoReUndo movement_undo_re_undo(5, 5);
    bool result;

    result = movement_undo_re_undo.Undo();
    assert(!result, "Yndo no ok");
    result = movement_undo_re_undo.Redo();
    assert(!result, "Redo no ok");
}

// Mover en las cuatro direcciones desde posiciones que lo permitan.
void Test2()
{
    MovementUndoReUndo movement_undo_re_undo(5, 5);
    bool result;

    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 1");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 2");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 3");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 4");
}

// Llegar a cada borde y rechazar un movimiento que intente atravesarlo.
void Test3()
{
    MovementUndoReUndo movement_undo_re_undo(5, 5);
    bool result;

    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 1");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 2");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 3");
    result = movement_undo_re_undo.Move(1, 0);
    assert(result, "move 4");
    result = movement_undo_re_undo.Move(0, 1);
    assert(result, "move 5");
    result = movement_undo_re_undo.Move(0, 1);
    assert(result, "move 6");
    result = movement_undo_re_undo.Move(0, 1);
    assert(result, "move 7");
    result = movement_undo_re_undo.Move(0, 1);
    assert(result, "move 8");
    result = movement_undo_re_undo.Move(-1, 0);
    assert(result, "move 9");
    result = movement_undo_re_undo.Move(-1, 0);
    assert(result, "move 10");
    result = movement_undo_re_undo.Move(-1, 0);
    assert(result, "move 11");
    result = movement_undo_re_undo.Move(-1, 0);
    assert(result, "move 12");
    result = movement_undo_re_undo.Move(0, -1);
    assert(result, "move 13");
    result = movement_undo_re_undo.Move(0, -1);
    assert(result, "move 14");
    result = movement_undo_re_undo.Move(0, -1);
    assert(result, "move 15");
    result = movement_undo_re_undo.Move(0, -1);
    assert(result, "move 16");
    result = movement_undo_re_undo.Move(0, -1);
    assert(!result, "move 17");
}

void Test4()
{
    MovementUndoReUndo movement_undo_re_undo(5, 5);
    bool result;

    result = movement_undo_re_undo.Move(1, 1);
    assert(!result);
    result = movement_undo_re_undo.Move(-1, 1);
    assert(!result);
    result = movement_undo_re_undo.Move(1, -1);
    assert(!result);
    result = movement_undo_re_undo.Move(-1, -1);
    assert(!result);
}

// Rechazar diagonales, saltos y movimientos sin desplazamiento sin cambiar el estado.

int main(int argc, char* argv[])
{
    Test1();
    Test2();
    Test3();
    Test4();
    //Samples();    

    return 0;
}
