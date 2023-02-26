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
        public bool isPlaying = true;
        int counter;
        int index;
        List<string> animation = new List<string>();

        public void playAnimation(int duration, Transform trans)
        {
            if (animation.Count <= 1 || !isPlaying) return;

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

        public void PlayAnimationOnce(int duration, Transform trans)
        {
            if (!isPlaying) return;

            if (index == 0) // play first frame if not played yet
            {
                trans.SpritePath = Bootstrap.getAssetManager().getAssetPath(animation[0]);
                index++;
                return;
            }

            counter++;
            if (counter % duration == 0)
            {
                counter = 0;
                if (index <= animation.Count - 1)
                {
                    isPlaying = true;
                    trans.SpritePath = Bootstrap.getAssetManager().getAssetPath(animation[index]);
                    index++;
                }
                else
                {
                    isPlaying = false; // exit loop when end of animation is reached
                }
            }
        }
        public void StopAnimation()
        {
            isPlaying = false;
        }
        public void StartAnimation()
        {
            isPlaying = true;
        }
    }

}

