using System;
using System.Numerics;
using Shard;

namespace GameAssignment
{
    class Platform : GameObject, InputListener, CollisionHandler
    {
        private int health;
        Vector2 TilePos;

        public int Health { get => health; set => health = value; }

        public override void initialize()
        {
            setPhysicsEnabled();
            
            MyBody.Mass = 1000;
            MyBody.Kinematic = true;
            MyBody.addRectCollider();
            addTag("Grass");
        }
        public Platform(int x, int y)
        {
            TilePos.X = x;
            TilePos.Y = y;

            this.Transform.X = x;
            this.Transform.Y = y;
        }

        public void handleInput(InputEvent inp, string eventType)
        {

        }


        public override void update()
        {
            //this.Transform.X = TilePos.X - Bootstrap.camPos.X;
            //this.Transform.Y = TilePos.Y - Bootstrap.camPos.Y;

            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("grass.png");
            Bootstrap.getDisplay().addToDraw(this);
            Console.WriteLine("platform: " + this.Transform.X + " " + this.Transform.Y);
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
            return "Grass: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}

