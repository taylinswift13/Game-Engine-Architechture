
using Shard;
using System;
using System.Collections.Generic;
namespace GameAssignment
{
    class Player : GameObject, InputListener, CollisionHandler
    {
        private int health;
        public int Health { get => health; set => health = value; }
        List<string> animation = new List<string>();
        public override void initialize()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("player_idle1.png");
            setPhysicsEnabled();
            MyBody.addRectCollider();

            MyBody.Mass = 1;
            MyBody.MaxForce = 15;
            MyBody.Drag = 0f;
            MyBody.UsesGravity = false;
            MyBody.StopOnCollision = false;
            MyBody.ReflectOnCollision = true;

            Transform.Scalex = 2;
            Transform.Scaley = 2;

            //animation test
            for (int i = 1; i <= 4; i++)
            {
                string idleAnimationIndex = "player_idle" + i + ".png";
                animation.Add(idleAnimationIndex);
            }
        }

        public void handleInput(InputEvent inp, string eventType)
        {

        }

        int index = 0; int counter = 0;
        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
            if (animation.Count <= 1) return;

            counter++;
            if (counter % 5 == 0)
            {
                counter = 0;
                if (index <= animation.Count - 1) { index++; }
            }
            if (index == animation.Count) { index = 0; }
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(animation[index]);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            Health -= 1;

            if (Health <= 0)
            {
                this.ToBeDestroyed = true;
            }
        }

        public void onCollisionExit(PhysicsBody x)
        {

        }

        public void onCollisionStay(PhysicsBody x)
        {
        }

        public override string ToString()
        {
            return "Player: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}
