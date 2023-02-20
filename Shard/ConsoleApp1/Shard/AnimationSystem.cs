using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard
{
    class AnimationSystem
    {
        int counter;
        int index;

        public AnimationSystem()
        {
            counter = 0;
            index = 0;
        }

        public void playAnimation(List<string> animationClip, int duration, Transform trans)
        {
            if (animationClip.Count <= 1) return;

            counter++;
            if (counter % duration == 0)
            {
                counter = 0;
                if (index <= animationClip.Count - 1) { index++; }
            }
            if (index == animationClip.Count) { index = 0; }

            if (index > animationClip.Count - 1) return;
            else trans.SpritePath = Bootstrap.getAssetManager().getAssetPath(animationClip[index]);
        }
    }
}
