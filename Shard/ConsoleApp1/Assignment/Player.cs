using SDL2;
using Shard;
using System;
using System.Collections.Generic;
namespace GameAssignment
{
    class Player : GameObject, InputListener, CollisionHandler
    {
        private int health;
        bool left, right;
        int wid;

        public int Health { get => health; set => health = value; }
        List<string> idleAnimationClip = new List<string>();
        List<string> runAnimationClip = new List<string>();
        public override void initialize()
        {
            left = false;
            right = false;

            this.Transform.X = 50.0f;
            this.Transform.Y = 600.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("player_idle1.png");
            this.Transform.Scalex = 3;
            this.Transform.Scaley = 3;

            //animation test
            loadAnimation("player_idle", 4, idleAnimationClip);
            loadAnimation("player_run", 6, runAnimationClip);

            Bootstrap.getInput().addListener(this);

            left = false;
            right = false;

            setPhysicsEnabled();

            MyBody.Mass = 1000;
            MyBody.MaxForce = 20;
            MyBody.Drag = 0.3f;
            MyBody.UsesGravity = true;
            MyBody.addRectCollider();

            wid = Bootstrap.getDisplay().getWidth();
        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                    this.Transform.Flip = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    this.Transform.Flip = true;
                }

            }
            else if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = false;
                }
            }
        }


        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);

            if (right || left) playAnimation(runAnimationClip, 7);
            else playAnimation(idleAnimationClip, 10);

        }

        public override void physicsUpdate()
        {
            double boundsx;
            if (left)
            {
                MyBody.addForce(this.Transform.Forward, -1 * 500f);
            }

            if (right)
            {
                MyBody.addForce(this.Transform.Forward, 500);
            }

            if (this.Transform.X < 0)
            {
                this.Transform.translate(-1 * Transform.X, 0);
            }

            boundsx = wid - (this.Transform.X + this.Transform.Wid);

            if (boundsx < 0)
            {
                this.Transform.translate(boundsx, 0);
            }

            Bootstrap.getDisplay().addToDraw(this);
        }


        public void loadAnimation(string fileName, int frames, List<string> animation)
        {
            for (int i = 1; i <= frames; i++)
            {
                string idleAnimationIndex = fileName + i + ".png";
                animation.Add(idleAnimationIndex);
            }
        }
        //how to make these two variable inside the function?
        int index = 0; int counter = 0;
        public void playAnimation(List<string> animationClip, int duration)
        {
            if (animationClip.Count <= 1) return;

            counter++;
            if (counter % duration == 0)
            {
                counter = 0;
                if (index <= animationClip.Count - 1) { index++; }
            }
            if (index == animationClip.Count) { index = 0; }

            if (index > animationClip.Count - 1) return;
            else this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(animationClip[index]);
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
