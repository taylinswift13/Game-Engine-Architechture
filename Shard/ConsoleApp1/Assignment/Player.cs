using SDL2;
using Shard;
using System;
using System.Collections.Generic;
namespace GameAssignment
{
    class Player : GameObject, InputListener, CollisionHandler
    {
        private int health;
        bool left, right, jumpUp, isJumping;
        double jumpCount;
        private double speed = 100, jumpSpeed = 260;
        int wid;

        public int Health { get => health; set => health = value; }
        List<string> idleAnimationClip = new List<string>();
        List<string> runAnimationClip = new List<string>();
        public override void initialize()
        {
            left = false;
            right = false;
            jumpUp = false;
            isJumping = false;

            this.Transform.X = 50.0f;
            this.Transform.Y = 400.0f;
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
            MyBody.Mass = 1;
            MyBody.UsesGravity = true;
            MyBody.StopOnCollision = true;
            //MyBody.addRectCollider((int)Transform.X, (int)Transform.Y + 1, 22, 23);
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
                    this.Transform.FlipHorizontal = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    this.Transform.FlipHorizontal = true;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE && !isJumping)
                {
                    jumpUp = true;
                }

                //inverse gravity
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_E)
                {
                    Bootstrap.gravityDir = -Bootstrap.gravityDir;
                    if (!Transform.FlipVertical) Transform.FlipVertical = true;
                    else Transform.FlipVertical = false;
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
            Bootstrap.getDisplay().showText("SPACE to jump", 10, 10, 20, System.Drawing.Color.White);
            Bootstrap.getDisplay().showText("E to inverse gravity", 10, 35, 20, System.Drawing.Color.White);
            if (right || left) playAnimation(runAnimationClip, 7);
            else playAnimation(idleAnimationClip, 10);

            if (jumpUp)
            {
                if (jumpCount < 0.3f)
                {
                    this.Transform.translate(0, -Bootstrap.gravityDir * jumpSpeed * Bootstrap.getDeltaTime());
                    jumpCount += Bootstrap.getDeltaTime();
                    isJumping = true;
                }
                else
                {
                    jumpCount = 0;
                    jumpUp = false;
                }
            }
        }

        public override void physicsUpdate()
        {
            double boundsx;
            if (left)
            {
                this.Transform.translate(-5, 0);
            }

            if (right)
            {
                this.Transform.translate(5, 0);
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
            isJumping = false;
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
            return "Player: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}
