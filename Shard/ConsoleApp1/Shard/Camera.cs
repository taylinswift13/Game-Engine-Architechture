using System;
using System.Numerics;

namespace Shard
{
    class Camera
    {
        public Vector2 Position;
        public Vector2 Size;

        public void FollowGameObject(Vector2 objPos)
        {
            // Set the camera position to be centered on the game object
            Position.X = objPos.X - (Size.X / 2);
            Position.Y = objPos.Y - (Size.Y / 2);

            // Clamp the camera's position to the game world
            if (Position.X < 0)
            {
                Position.X = 0;
            }
            else if (Position.X > Size.X)
            {
                Position.X = Size.X;
            }

            if (Position.Y < 0)
            {
                Position.Y = 0;
            }
            else if (Position.Y > Size.Y)
            {
                Position.Y = Size.Y;
            }

            Bootstrap.camPos = Position;
            Bootstrap.camSize = Size;
        }
    }
}
