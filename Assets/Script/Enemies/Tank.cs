namespace Script.Enemies
{
    public class Tank: Base
    {
        protected override void Start()
        {
            base.Start();
            type = Type.Tank;
        }
    }
}