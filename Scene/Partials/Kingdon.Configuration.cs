using Formula.Interfaces;

namespace Formula.Scene;

partial class Kingdon
{
    public Dictionary<Guid, IObject> Objects = [];
    private Dictionary<(int,int), IObject> GridObjects = [];
    
    public void Loop(object? sender, EventArgs e)
    {
        foreach(var obj in Objects.Values) obj.SavePosition();
        foreach(var obj in Objects.Values) obj.Update(this);

        MoveObjects();
        DestroyObjects();
        SpawnObjects();

        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        foreach (var obj in Objects.Values)
            obj.Draw(e.Graphics);
        
        e.Graphics.DrawString(Objects.Count.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.White, 1, 1);
    }
}