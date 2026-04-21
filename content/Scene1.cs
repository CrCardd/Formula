public class Scene1 : SceneMap
{
    public Scene1()
    {
        Initialize(25,25);
        BaseOBject.Size = 15;
        New(new(1,1, color: Color.Purple, behavior: new Strategy1()));
    }
}