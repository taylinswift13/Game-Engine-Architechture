using System;
using System.Numerics;

namespace Shard
{
    class Camera
    {
        public Vector2 Position;
        public Vector2 Size;
        private Vector2 previousPosition;
        private Vector2 bgSize = new Vector2(992,544);

        public void FollowGameObject(Vector2 objPos, float smoothing)
        {
            // Calculate the target position for the camera
            Vector2 targetPos = new Vector2(objPos.X - (Size.X / 2) / Bootstrap.CamViewScale, objPos.Y - (Size.Y / 2) / Bootstrap.CamViewScale);

            // Apply camera smoothing
            Position = Vector2.Lerp(previousPosition, targetPos, smoothing);
            previousPosition = Position;

            // Clamp the camera's position to the game world
            Position.X = Math.Max(0, Math.Min(Position.X, 2 * bgSize.X - Size.X));
            Position.Y = Math.Max(0, Math.Min(Position.Y, bgSize.Y - Size.Y));

            // Update the camera position and size in the Bootstrap class
            Bootstrap.camPos = Position;
            Bootstrap.camSize = Size;
        }
    }
}

