using System.Drawing;

public class Scene1 : SceneMap
{
    public Scene1()
    {
        Initialize(25,25);
        BaseObject.Size = 15;
        New(1,1, color: Color.Purple, behavior: new Strategy1());
    }
}