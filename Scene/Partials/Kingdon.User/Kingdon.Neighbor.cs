using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    public IEnumerable<T> RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject
    => getplace.RadiusAreaObjects<T>(x,y,n);
    public IEnumerable<BaseOBject> RadiusAreaObjects(double x, double y, int n=1) 
    => RadiusAreaObjects<BaseOBject>(x,y,n);

    public IEnumerable<T> NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject
    => getplace.NeighborObjects<T>(x,y,diagonal);
    public IEnumerable<BaseOBject> NeighborObjects(double x, double y, bool diagonal=false) 
    => NeighborObjects<BaseOBject>(x,y);

    public Dictionary<Vector2D, IEnumerable<T>> GetGrid<T>(double x, double y, bool diagonal=false) where T : BaseOBject
    => getplace.GetGrid<T>(x,y,diagonal);
    public Dictionary<Vector2D, IEnumerable<BaseOBject>> GetGrid(double x, double y, bool diagonal=false)
    => GetGrid<BaseOBject>(x,y,diagonal);
}