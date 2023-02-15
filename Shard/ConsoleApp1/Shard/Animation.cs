using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard
{
    abstract class Animation
    {
        GameObject owner;
        //public Animation(GameObject ow)
        //{
        //   owner = ow;
        //}

        int counter = 0; int index = 0;
        public void playAnimation(List<string> animationClip, float speed, GameObject gameObject)
        {
            if (animationClip.Count <= 1) return;
            gameObject.Transform.SpritePath = animationClip[index];
            counter++;
            if (counter % speed == 0)
            {
                counter = 0;
                if (index <= animationClip.Count - 1)
                {
                    index++;
                }
            }
            if (index == animationClip.Count)
            {
                index = 0;
            }

        }
    }
}
