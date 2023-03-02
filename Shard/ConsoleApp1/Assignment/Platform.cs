using System;
using System.Numerics;
using Shard;

namespace GameAssignment
{
    class Platform : GameObject, InputListener, CollisionHandler
    {
        private int health;
        private bool physicsEnabled; // new field to store whether physics is enabled or not
        private int type;
        public int Health { get => health; set => health = value; }

        public Platform(int x, int y, int type, bool physicsEnabled  = true) // added physicsEnabled parameter with default value of true
        {
            this.physicsEnabled = physicsEnabled; // store whether physics is enabled or not
            Transform.X = x / Bootstrap.CamViewScale;
            Transform.Y = y / Bootstrap.CamViewScale;
            this.type = type;
        }

        public override void initialize()
        {
            Transform.Scalex = 0.5f;
            Transform.Scaley = 0.5f;

            if (physicsEnabled) // check if physics is enabled before setting up the physics body
            {
                addTag("Platform");
                setPhysicsEnabled();
                MyBody.Mass = 1;
                MyBody.Kinematic = true;
                MyBody.addRectCollider();
            }
        }

        public void handleInput(InputEvent inp, string eventType)
        {
        }

        public override void update()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("Tile_" + type + ".png");
            Bootstrap.getDisplay().addToDraw(this);
            Console.WriteLine(physicsEnabled);
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
            return "Platform: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}


