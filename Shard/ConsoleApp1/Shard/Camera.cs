using System;
using System.Numerics;

namespace Shard
{
    class Camera
    {
        public Vector2 Position;
        public Vector2 Size;

        public void FollowGameObject(GameObject gameObject)
        {
            // Set the camera position to be centered on the game object
            Position.X = gameObject.Transform.X - (Size.X / 2);
            Position.Y = gameObject.Transform.Y - (Size.Y / 2);

            // Clamp the camera's position to the game world
            Position.X = Math.Max(0, Math.Min(Position.X, Bootstrap.getDisplay().getWidth() - Size.X));
            Position.Y = Math.Max(0, Math.Min(Position.Y, Bootstrap.getDisplay().getHeight() - Size.Y));

            Bootstrap.Position = Position;
            Bootstrap.Size = Size;
        }
    }
}
