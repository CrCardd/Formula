using System;
using System.Collections.Generic;
using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    public bool isValid(double x, double y) => x>=0 && x<Width && y>=0 && y < Height;
}