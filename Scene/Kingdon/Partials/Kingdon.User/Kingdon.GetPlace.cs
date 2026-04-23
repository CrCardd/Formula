using System.Collections.Generic;
using Formula.Objects;

namespace Formula.Scene;

public partial class SceneMap
{
    public bool isValid(double x, double y) => getPlace!.isValid(x,y);
    
    public bool isValid(Vector2D position) => isValid(position.X,position.Y);

    public IEnumerable<T> GetPlace<T>(double x, double y) where T : BaseOBject 
    => getPlace!.GetPlace<T>(x,y);
    public IEnumerable<BaseOBject> GetPlace(double x, double y) 
    => GetPlace<BaseOBject>(x,y);

    public IEnumerable<T> GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    => getPlace!.GetPlace<T>(x,y);
    public IEnumerable<BaseOBject>? GetPlaceOrDefault(double x, double y) 
    => GetPlaceOrDefault<BaseOBject>(x,y);
}