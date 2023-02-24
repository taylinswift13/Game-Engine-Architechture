
using Shard;

namespace GameAssignment
{
    class Platform : GameObject, InputListener, CollisionHandler
    {
        private int health;
        int type;
        public int Health { get => health; set => health = value; }

        public override void initialize()
        {
            setPhysicsEnabled();

            MyBody.Mass = 1;
            MyBody.Kinematic = true;

            MyBody.addRectCollider();

            this.Transform.Scalex = 0.5f;
            this.Transform.Scaley = 0.5f;
            addTag("Platform");

        }
        public Platform(int x, int y, int type)
        {
            this.Transform.X = x;
            this.Transform.Y = y;
            this.type = type;
        }

        public void handleInput(InputEvent inp, string eventType)
        {

        }


        public override void update()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("grass" + type + ".png");
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
            return "Platform: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}

