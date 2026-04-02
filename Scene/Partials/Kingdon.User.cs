using Formula.Interfaces;
using Formula.Math;

namespace Formula.Scene;

partial class Kingdon
{
    private Queue<IObject> toDestroy = [];
    private Queue<IObject> toSpawn = [];
    
    public Kingdon(int w, int h)
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;

        this.Width = IObject.Size * w;
        this.Height = IObject.Size * h;
        this.ClientSize = new Size(this.Width,this.Height);
    
        System.Windows.Forms.Timer t = new();
        t.Interval = 16; // ~60 FPS
        t.Tick += (s, e) => Loop(s,e);
        
        t.Start();

    }

    #region Base functions
    public void New(IObject obj){
        int maxGridX = ClientSize.Width / IObject.Size;
        int maxGridY = ClientSize.Height / IObject.Size;
        
        if (obj.Position.X < 0 || obj.Position.X >= maxGridX) return;
        if (obj.Position.Y < 0 || obj.Position.Y >= maxGridY) return;

        toSpawn.Enqueue(obj);
    }
    public void Destroy(IObject obj) => toDestroy.Enqueue(obj);
    public IReadOnlyCollection<IObject> GetObjects => Objects.Values.ToList();
    #endregion
    
    #region Util functions
    public bool isFree(int x, int y) => GetPlace(x,y) != null;
    public IObject? GetPlace(int x, int y) => Objects.Values.FirstOrDefault(o => o.Position.X == x && o.Position.Y == y);
    public IObject? GetPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue((position.X, position.Y), out var obj)) return obj;
        return null;
    }
    public Vector2D? GetRandomFreeNeighboorPlace(IWorld world, int x, int y)
    {
        List<Tuple<int,int>> offsets = [
            new(-1,0),
            new(0,1),
            new(1,0),
            new(0,-1),
        ];
        
        return GetRandomFreeNeighboorPlace(world, offsets, y, x);
    }
    public Vector2D? GetRandomFreeNeighboorPlace(IWorld world, List<Tuple<int,int>> offsets, int x, int y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght-1);
        IObject? o = world.GetPlace(y+offsets[r].Item1, x+offsets[r].Item2);

        if(o is null)
            return new(y+offsets[r].Item1, x+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandomFreeNeighboorPlace(world, offsets, x, y);
    }
    
    #endregion
}