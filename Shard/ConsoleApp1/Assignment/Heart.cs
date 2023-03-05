using System;
using System.Numerics;
using Shard;

namespace GameAssignment
{
    class Heart : GameObject, InputListener, CollisionHandler
    {

        public Heart(float x, float y) // added physicsEnabled parameter with default value of true
        {
            Transform.X = x;
            Transform.Y = y;
        }

        public override void initialize()
        {
            Transform.Scalex = 0.5f;
            Transform.Scaley = 0.5f;
            Transform.IsUI = true;
        }

        public void handleInput(InputEvent inp, string eventType)
        {
        }

        public override void update()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("heart.png");
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
        }

        public void onCollisionExit(PhysicsBody x)
        {
        }

        public void onCollisionStay(PhysicsBody x)
        {
        }

        public override string ToString()
        {
            return "Heart: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}


