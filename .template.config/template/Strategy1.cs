using System.Linq;
using System.Net;
using Formula.Interfaces;

public class Strategy1 : Behavior
{
    public override void Execute(Cell obj, IWorld world)
    {
        obj.X = 1; // Aleatório
        obj.Y = 1; // Aleatório
    }
}