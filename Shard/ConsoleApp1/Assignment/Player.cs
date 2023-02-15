
using SDL2;
using Shard;

namespace GameAssignment
{
    class Player : GameObject, InputListener, CollisionHandler
    {
        private int health;

        public int Health { get => health; set => health = value; }

        public override void initialize()
        {
            Bootstrap.getInput().addListener(this);
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("Untitled.png");
            //setPhysicsEnabled();
            //MyBody.addRectCollider();

            //MyBody.Mass = 1;
            //MyBody.MaxForce = 15;
            //MyBody.Drag = 0f;
            //MyBody.UsesGravity = false;
            //MyBody.StopOnCollision = false;
            //MyBody.ReflectOnCollision = true;

            Transform.Scalex = 1;
            Transform.Scaley = 1;

            //Transform.rotate(90);
        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE)
                {
                    Bootstrap.getSound().playSound("fire.wav");
                }
            }
        }


        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
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
