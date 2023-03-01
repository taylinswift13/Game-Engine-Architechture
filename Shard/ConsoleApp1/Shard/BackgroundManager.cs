using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class BackgroundManager
    {
        private readonly List<GameObject> backgrounds;
        private readonly Vector2 bgSize;
        private readonly List<Vector2> originalPositions;

        public BackgroundManager(Vector2 spriteSize)
        {
            this.bgSize = spriteSize;
            backgrounds = new List<GameObject>();
            originalPositions = new List<Vector2>();
        }

        public void AddBackground(string spritePath, int posX, int posY, int scaleX, int scaleY)
        {
            var background = new GameObject
            {
                Transform =
                {
                    SpritePath = Bootstrap.getAssetManager().getAssetPath(spritePath),
                    Wid = (int)bgSize.X,
                    Ht = (int)bgSize.Y,
                    Scalex = scaleX,
                    Scaley = scaleY,
                    X = posX / Bootstrap.CamViewScale,
                    Y = posY
                }
            };
            backgrounds.Add(background);
            originalPositions.Add(new Vector2(background.Transform.X, background.Transform.Y));
        }

        public void Update(float speed)
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                var background = backgrounds[i];
                var originalPosition = originalPositions[i];
                originalPositions[i] = new Vector2(originalPosition.X, background.Transform.Y);
                if (background.Transform.X > originalPosition.X - (bgSize.X / Bootstrap.CamViewScale))
                {
                    background.Transform.X -= speed;
                }
                if (background.Transform.X <= originalPosition.X - (bgSize.X / Bootstrap.CamViewScale))
                {
                    background.Transform.X = originalPosition.X;
                }
            }
        }

        public void Draw()
        {
            foreach (var background in backgrounds)
            {
                Bootstrap.getDisplay().addToDraw(background);
            }
        }
    }
}


