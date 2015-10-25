using Mono.Cecil;
using UnityEngine;

namespace Assets.Blocks
{
    public class Block
    {
        public Chunk _chunk;
        private int _light;

        public virtual int Light
        {
            get
            {
                return _light;
            }
            set
            {
                int light = Mathf.Clamp(value, MinLight, MaxLight);
                if (_light != light)
                {
                    _light = light;
                    if (_chunk != null)
                        _chunk.SetDirty();
                }
            }
        }

        public static int MinLight { get { return 50; } }
        public static int MaxLight { get { return 255; } }

        public Block()
        {
            _light = MinLight;
        }

        public virtual bool IsSolid()
        {
            return true;
        }
    }
}