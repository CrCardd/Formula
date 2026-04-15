using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    
    private IEnumerable<T> _RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject
    {   
        int startX = (int)x-n;
        int startY = (int)y-n;
        ICollection<T> values = [];

        for(int i=startY; i<=y+n;i++)
        {
            for(int j=startX; j<=x+n;j++)
            {
                if(i==y && j==x) continue;
                if(!isValid(i,j)) continue;
                var pos = GetPlaceOrDefault<T>(j,i);
                if(pos is null) continue;

                foreach(var obj in pos)
                    yield return obj;
            }
        }
    }
    public IReadOnlyCollection<T> RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject => _RadiusAreaObjects<T>(x,y,n).ToList();    
    public IReadOnlyCollection<BaseOBject> RadiusAreaObjects(double x, double y, int n=1) => RadiusAreaObjects<BaseOBject>(x,y,n);


    private IEnumerable<T> _NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject
    {
        int startX = (int)x-1;
        int startY = (int)y-1;

        for(int i=startY; i<-y+1;i++)
            for(int j=startX; j<=x+1;j++)
            {
                if(!diagonal && i!=y && j!=x) continue;
                if(i==y && j==x) continue;
                if(!isValid(j,i)) continue;
                var pos = GetPlaceOrDefault<T>(j,i);
                if(pos is null) continue;

                foreach(var obj in pos)
                    yield return obj;
            }
    }
    public IReadOnlyCollection<T> NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject 
    => _NeighborObjects<T>(x,y,diagonal).ToList();
    public IReadOnlyCollection<BaseOBject> NeighborObjects(double x, double y, bool diagonal=false) => NeighborObjects<BaseOBject>(x,y);

    public IEnumerable<Vector2D> GetGrid(double x, double y, bool diagonal=false)
    {
        int startX = (int)x-1;
        int startY = (int)y-1;

        for(int i=startY; i<=y+1;i++)
            for(int j=startX; j<=x+1;j++)
            {
                if(!diagonal && i!=y && j!=x) continue;
                if(!isValid(j,i)) continue;

                yield return new(j,i);
            }
    }
}