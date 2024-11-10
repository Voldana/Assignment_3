namespace Script.Enemies
{
    public class Mortar: Base
    {
        protected override void Start()
        {
            base.Start();
            type = Type.Mortar;
        }
    }
}