using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.IO;

namespace Shard
{
    class AnimationSystem
    {
        int counter;
        int index;
        List<string> animation = new List<string>();

        public void playAnimation(int frames, int duration, Transform trans)
        {
            if (animation.Count <= 1) return;

            counter++;
            if (counter % duration == 0)
            {
                counter = 0;
                if (index <= animation.Count - 1) { index++; }
            }
            if (index == animation.Count) { index = 0; }

            if (index > animation.Count - 1) return;
            else trans.SpritePath = Bootstrap.getAssetManager().getAssetPath(animation[index]);
        }

        public void loadAnimation(string fileName, int frames)
        {
            for (int i = 1; i <= frames; i++)
            {
                string frame = fileName + i + ".png";
                animation.Add(frame);
            }
        }
    }
}

