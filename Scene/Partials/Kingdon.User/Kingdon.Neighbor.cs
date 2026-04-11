using System;
using System.Collections.Generic;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    
    public IEnumerable<T> NNeighborCells<T>(double x, double y, int n=1) where T : BaseOBject
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
                var obj = GetPlaceOrDefault<T>(j,i);
                if(obj is null) continue;

                yield return obj;
            }
        }
    }
    public IEnumerable<BaseOBject> NNeighborCells(double x, double y, int n=1) => NNeighborCells<BaseOBject>(x,y,n);

    public IEnumerable<T> NeighborCells<T>(double x, double y, bool diagonal=false) where T : BaseOBject
    {
        int startX = (int)x-1;
        int startY = (int)y-1;

        for(int i=startY; i<-y+1;i++)
            for(int j=startX; j<=x+1;j++)
            {
                if(!diagonal && i!=y && j!=x) continue;
                if(i==y && j==x) continue;
                if(!isValid(j,i)) continue;
                var obj = GetPlaceOrDefault<T>(j,i);
                if(obj is null) continue;

                yield return obj;
            }
    }
    public IEnumerable<BaseOBject> NeighborCells(double x, double y, bool diagonal=false) => NeighborCells<BaseOBject>(x,y);

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

    public Vector2D? GetRandom4FreeNeighborPlace(double x, double y)
    {
        List<Tuple<int, int>> offsets = [
                new(-1, 0), new(1, 0), new(0, -1), new(0, 1)
        ];
        return GetRandom4FreeNeighborPlace(offsets, x, y);
    }
    public Vector2D? GetRandom4FreeNeighborPlace(List<Tuple<int,int>> offsets, double x, double y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght);
        
        if(GetPlaceOrDefault(x+offsets[r].Item1, y+offsets[r].Item2) == null)
            return new(x+offsets[r].Item1, y+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandom4FreeNeighborPlace(offsets, x, y);
    }

    public Vector2D? GetRandom8FreeNeighborPlace(double x, double y)
    {
        List<Tuple<int, int>> offsets = [
                new(-1, 0), new(1, 0), new(0, -1), new(0, 1),
                new(-1, -1), new(1, 1), new(1, -1), new(-1, 1),
        ];
        return GetRandom8FreeNeighborPlace(offsets, x, y);
    }
    public Vector2D? GetRandom8FreeNeighborPlace(List<Tuple<int,int>> offsets, double x, double y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght);
        
        if(GetPlaceOrDefault(x+offsets[r].Item1, y+offsets[r].Item2) == null)
            return new(x+offsets[r].Item1, y+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandom8FreeNeighborPlace(offsets, x, y);
    }
}