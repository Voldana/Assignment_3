namespace Script.Enemies
{
    public class Mine: Base
    {
        protected override void Start()
        {
            base.Start();
            type = Type.Mine;
        }
    }
}