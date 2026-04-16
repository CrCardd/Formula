using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    public IEnumerable<T> GetPlace<T>(double x, double y) where T : BaseOBject 
    => getplace.GetPlace<T>(x,y);
    public IEnumerable<BaseOBject> GetPlace(double x, double y) 
    => GetPlace<BaseOBject>(x,y);

    public IEnumerable<T>? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    => getplace.GetPlaceOrDefault<T>(x,y);
    public IEnumerable<BaseOBject>? GetPlaceOrDefault(double x, double y) 
    => GetPlaceOrDefault<BaseOBject>(x,y);
}