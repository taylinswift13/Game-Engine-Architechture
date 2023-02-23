using SDL2;
using Shard;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameAssignment
{
    class Player : GameObject, InputListener, CollisionHandler
    {
        int health, wid;
        bool left, right, jumpUp, isJumping;
        double jumpCount, jumpSpeed = 350;
        AnimationSystem idleAni;
        AnimationSystem runAni;
        AnimationSystem hurtAni;
        AnimationSystem jumpAni;

        public int Health { get => health; set => health = value; }
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

            idleAni = new AnimationSystem();
            idleAni.loadAnimation("player_idle", 4);
            runAni = new AnimationSystem();
            runAni.loadAnimation("player_run", 6);
            hurtAni = new AnimationSystem();
            hurtAni.loadAnimation("player_hurt", 2);
            jumpAni = new AnimationSystem();
            jumpAni.loadAnimation("player_jump", 2);

            setPhysicsEnabled();
            MyBody.Mass = 1;
            MyBody.UsesGravity = true;
            MyBody.StopOnCollision = true;
            MyBody.addRectCollider();

            wid = Bootstrap.getDisplay().getWidth();
            Bootstrap.getInput().addListener(this);
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
            Bootstrap.playerPos = new Vector2(this.Transform.X, this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
            Bootstrap.getDisplay().showText("SPACE to jump", 10, 10, 20, System.Drawing.Color.White);
            Bootstrap.getDisplay().showText("E to inverse gravity", 10, 35, 20, System.Drawing.Color.White);
            if (!isJumping)
            {
                if (right || left) runAni.playAnimation(5, this.Transform);
                else idleAni.playAnimation(8, this.Transform);
            }


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
