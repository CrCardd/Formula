using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IEvents
{
    public void OnMouseDown(MouseArgs e);
    public void OnMouseUp(MouseArgs e);
    public void OnMouseMove(MouseArgs e);
    public void OnKeyDown(KeyEventArgs e);
    public bool IsKeyDown(Keys key);


    public void ApplyAll(Action<BaseOBject> apply);
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject;
    public void DestroyAllObjects();
}