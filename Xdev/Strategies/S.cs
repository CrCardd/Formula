// using Formula.Interfaces;
// using Formula.Objects;

// public class S : Behavior
// {
//     public override void Execute(BaseOBject obj, IWorld world)
//     {
//         if(world.isValid(obj.X, obj.Y+1) && world.isFree(obj.X, obj.Y+1))
//             obj.Y++;
//         var pos = world.GetRandom8FreeNeighborPlace(obj.X,obj.Y);
//         if(pos is null || pos.Value.Y < obj.Y || pos.Value.Y == obj.Y)
//             return;
//         world.New(new BaseOBject(pos.Value.X, pos.Value.Y, behavior: this));
//     }
// }