using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IEvents
{
    public void OnMouseDown(IWorld world, MouseArgs e);
    public void OnMouseUp(IWorld world, MouseArgs e);
    public void OnMouseMove(IWorld world, MouseArgs e);
    public void OnKeyDown(KeyEventArgs e);

    public void ApplyAll(Action<BaseOBject> apply);
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject;
    public void DestroyAllObjects();
}