using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    public IReadOnlyCollection<T> GetPlace<T>(double x, double y) where T : BaseOBject 
    => GridObjects[((int)x, (int)y)]
        .Where(pos => typeof(T) == pos.GetType())
        .Select(pos => (T)pos.Shadow!).ToList()!;
    public IReadOnlyCollection<BaseOBject> GetPlace(double x, double y) => GetPlace<BaseOBject>(x,y);

    public IReadOnlyCollection<T>? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var pos)) 
            return pos
                .Where(p => typeof(T) == p.GetType())
                .Select(p => (T)p.Shadow!)
                .ToList();
        return null;
    }
    public IReadOnlyCollection<BaseOBject>? GetPlaceOrDefault(double x, double y) => GetPlaceOrDefault<BaseOBject>(x,y);

}