﻿
using Shard;

namespace GameAssignment
{
    class Platform : GameObject, InputListener, CollisionHandler
    {
        private int health;

        public int Health { get => health; set => health = value; }

        public override void initialize()
        {
            setPhysicsEnabled();

            MyBody.Mass = 1;
            MyBody.Kinematic = true;

            MyBody.addRectCollider();

            addTag("Platform");

        }
        public Platform(int x, int y)
        {
            this.Transform.X = x;
            this.Transform.Y = y;
        }

        public void handleInput(InputEvent inp, string eventType)
        {

        }


        public override void update()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("grass.png");
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
            MyBody.stopForces();
        }

        public override string ToString()
        {
            return "Platform: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}

