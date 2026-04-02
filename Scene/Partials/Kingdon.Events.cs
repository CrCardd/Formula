using Formula.Interfaces;

namespace Formula.Scene;

partial class Kingdon
{
    public Action<int, int>? OnGridClick;
    protected override void OnMouseDown(MouseEventArgs e)
    {
        int gridX = e.X / IObject.Size;
        int gridY = e.Y / IObject.Size;
        OnGridClick?.Invoke(gridX, gridY);
    }
}