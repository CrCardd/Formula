using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

namespace Formula.Scene.GetPlaceStrategies;

public class GetReal(SceneMap sceneMap) : IInteract
{
    public MouseArgs? MouseArgs {get;set;} = sceneMap.MouseArgs;

    public Dictionary<Vector2D, IEnumerable<T>> GetGrid<T>(double x, double y, bool diagonal = false) where T : BaseOBject
    {
        Dictionary<Vector2D, IEnumerable<T>> positions = [];
        int startX = (int)x-1;
        int startY = (int)y-1;

        for(int i=startY; i<=y+1;i++)
            for(int j=startX; j<=x+1;j++)
            {
                if(!diagonal && i!=y && j!=x) continue;
                if(!isValid(j,i)) continue;

                positions.Add((j,i), GetPlaceOrDefault<T>(j,i) ?? []);
            }
        return positions;
    }
    public Dictionary<Vector2D, IEnumerable<BaseOBject>> GetGrid(double x, double y, bool diagonal = false)
    => GetGrid<BaseOBject>(x,y,diagonal);


    public IEnumerable<T> GetPlace<T>(double x, double y) where T : BaseOBject
    => sceneMap.GridObjects[((int)x, (int)y)].Select(pos => (T)pos).ToList();
    public IEnumerable<BaseOBject> GetPlace(double x, double y) => GetPlace<BaseOBject>(x,y);


    public IEnumerable<T>? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    {
        if (sceneMap.GridObjects.TryGetValue(((int)x, (int)y), out var pos)) 
            return pos
                .Where(p => p is T)
                .Select(p => (T)p)
                .ToList();
        return null;
    }
    public IEnumerable<BaseOBject>? GetPlaceOrDefault(double x, double y)
    => GetPlaceOrDefault<BaseOBject>(x,y);


    public IEnumerable<T> NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject
    {
        int cx = (int)x;
        int cy = (int)y;

        int startX = cx - 1;
        int startY = cy - 1;
        for (int i = startY; i <= cy+1; i++)
            for (int j = startX; j <= cx+1; j++)
            {
                if (!diagonal && i != cy && j != cx) continue;
                if (i == cy && j == cx) continue;
                if (!isValid(j, i)) continue;

                var pos = GetPlaceOrDefault<T>(j, i);
                if (pos is null) continue;

                foreach (var obj in pos)
                    yield return obj;
            }
    }
    public IEnumerable<BaseOBject> NeighborObjects(double x, double y, bool diagonal = false)
    => NeighborObjects<BaseOBject>(x,y,diagonal);

    public IEnumerable<T> RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject
    {
        int startX = (int)x-n;
        int startY = (int)y-n;

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
    public IEnumerable<BaseOBject> RadiusAreaObjects(double x, double y, int n) => RadiusAreaObjects<BaseOBject>(x,y,n);

    public bool isValid(double x, double y) => x>=0 && x<sceneMap.Width && y>=0 && y <sceneMap.Height;
    public bool isValid(Vector2D position) => isValid(position.X,position.Y);

    public T New<T>(T obj) where T : BaseOBject
    {
        
        if (obj.X < 0 || obj.X >= sceneMap.Width) throw new Exception("Invalid position to spawn!");
        if (obj.Y < 0 || obj.Y >= sceneMap.Height) throw new Exception("Invalid position to spawn!");

        sceneMap.ApplySpawn(obj, ((int)obj.X, (int)obj.Y));
        return obj;
    }
    public BaseOBject New(BaseOBject obj) => New<BaseOBject>(obj);
    
    public void Destroy(BaseOBject obj) => sceneMap.ApplyDestroy(obj, ((int)obj.X, (int)obj.Y));
}