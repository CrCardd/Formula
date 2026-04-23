using System.Collections.Generic;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IInteract
{
    public IEnumerable<BaseOBject> GetPlace(double x, double y);
    public IEnumerable<T> GetPlace<T>(double x, double y) where T : BaseOBject;
    public IEnumerable<T> RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject;
    public IEnumerable<BaseOBject> RadiusAreaObjects(double x, double y, int n);
    public IEnumerable<T> NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject;
    public IEnumerable<BaseOBject> NeighborObjects(double x, double y, bool diagonal=false);
    public Dictionary<Vector2D, IEnumerable<T>> GetGrid<T>(double x, double y, bool diagonal=false) where T : BaseOBject;
    public Dictionary<Vector2D, IEnumerable<BaseOBject>> GetGrid(double x, double y, bool diagonal=false);

    public bool isValid(double x, double y);
    public bool isValid(Vector2D position);

    public BaseOBject New(BaseOBject obj);

    public T New<T>(T obj) where T : BaseOBject;
    public void Destroy(BaseOBject obj);

    public MouseArgs? MouseArgs {get;set;}
}