using System;
using System.Numerics;

namespace Shard
{
    class Camera
    {
        public Vector2 Position;
        public Vector2 Size;

        public void FollowGameObject(Vector2 objPos, float smoothing)
        {
            // Calculate the target position for the camera
            Vector2 targetPos = new Vector2(objPos.X - (Size.X / 2) / Bootstrap.CamViewScale, objPos.Y - (Size.Y / 2) / Bootstrap.CamViewScale);

            // Interpolate between the current position and the target position
            Position = Vector2.Lerp(Position, targetPos, smoothing);

            // Clamp the camera's position to the game world
            Position.X = Math.Max(0, Math.Min(Position.X, Size.X));
            Position.Y = Math.Max(0, Math.Min(Position.Y, Size.Y));

            // Update the camera position and size in the Bootstrap class
            Bootstrap.camPos = Position;
            Bootstrap.camSize = Size;

        }
    }
}
