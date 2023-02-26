
using Shard;
using System.Numerics;

namespace GameAssignment
{
    class Bush : GameObject, InputListener, CollisionHandler
    {
        private int health;
        private float distance;
        private int range;
        private bool triggered, direction;
        public int Health { get => health; set => health = value; }

        public override void initialize()
        {
            triggered = false;
            direction = false;
            setPhysicsEnabled();
            distance = 0;
            range = 150;
            MyBody.Mass = 1;
            this.Transform.Scalex = 1.5f;
            this.Transform.Scaley = 1.5f;
            MyBody.addRectCollider();
            MyBody.Kinematic = true;

            addTag("Bush");

        }
        public Bush(int x, int y)
        {
            this.Transform.X = x;
            this.Transform.Y = y;
        }

        public void handleInput(InputEvent inp, string eventType)
        {
        }


        public override void update()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("bush.png");
            Bootstrap.getDisplay().addToDraw(this);

            if (triggered)
            {
                if (direction)//player on the right side
                {
                    this.Transform.translate(2, 0);
                }
                else //player on the left side
                {
                    this.Transform.translate(-2, 0);
                }
            }
            else
            {
                distance = Vector2.Distance(Bootstrap.playerPos, new(this.Transform.X, this.Transform.Y));
                if (distance <= range)
                {
                    direction = Bootstrap.playerPos.X - this.Transform.X >= 0 ? true : false;
                    triggered = true;
                }
            }
        }

        public void onCollisionEnter(PhysicsBody other)
        {
            if (other.Parent.checkTag("Player"))
            {
                ToBeDestroyed = true;
            }
        }

        public void onCollisionExit(PhysicsBody other)
        {
            if (other.Parent.checkTag("Platform"))
            {
                ToBeDestroyed = true;
            }
        }

        public void onCollisionStay(PhysicsBody x)
        {
        }

        public override string ToString()
        {
            return "Bush: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}

