namespace Assets.Blocks
{
    public class BlockAir : Block
    {
        public override int Light
        {
            get { return 255; }
        }

        public override bool IsSolid()
        {
            return false;
        }
    }
}
