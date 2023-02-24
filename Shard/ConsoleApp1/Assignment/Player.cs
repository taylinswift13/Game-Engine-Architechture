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
        bool left, right, jumpUp, inTheAir, stopForce;
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
            inTheAir = false;
            stopForce = false;

            this.Transform.X = 0.0f;
            this.Transform.Y = 0.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("player_idle1.png");
            this.Transform.Scalex = 2;
            this.Transform.Scaley = 2;

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

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE && !inTheAir)
                {
                    jumpUp = true;
                }

                //inverse gravity
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_E && !inTheAir)
                {
                    Bootstrap.gravityDir = -Bootstrap.gravityDir;
                    if (!Transform.FlipVertical) Transform.FlipVertical = true;
                    else Transform.FlipVertical = false;
                    inTheAir = true;
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
            if (!inTheAir)
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
                    inTheAir = true;
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
            if (!stopForce)
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
            }

            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            inTheAir = false;
            stopForce = true;
        }

        public void onCollisionExit(PhysicsBody x)
        {
            stopForce = false;
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
