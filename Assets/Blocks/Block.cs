namespace Assets.Blocks
{
    public class Block
    {
        public virtual int Light { get; set; }

        public Block()
        {
            Light = 100;
        }

        public virtual bool IsSolid()
        {
            return true;
        }
    }
}