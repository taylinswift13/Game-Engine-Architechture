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
        bool left, right, jumpUp, inTheAir, stopForce, isHurt;
        double jumpCount, jumpSpeed = 275;
        AnimationSystem idleAni;
        AnimationSystem runAni;
        AnimationSystem hurtAni;
        AnimationSystem jumpAni;

        SoundManager sm = new SoundManager();
        int sound1Channel;
        SoundStatus sound1Status;
        string hurt = "hurt.wav";

        public int Health { get => health; set => health = value; }
        public override void initialize()
        {
            left = false;
            right = false;
            jumpUp = false;
            inTheAir = false;
            stopForce = false;
            isHurt = false;
            health = 3;
            this.Transform.X = 0.0f / Bootstrap.CamViewScale;
            this.Transform.Y = 325.0f / Bootstrap.CamViewScale;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("player_idle1.png");
            this.Transform.Scalex = 1.25f;
            this.Transform.Scaley = 1.25f;

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
            //MyBody.Kinematic = true;

            sm.initializeAudioSystem();

            wid = Bootstrap.getDisplay().getWidth();
            Bootstrap.getInput().addListener(this);
            addTag("Player");
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
                if (right || left) runAni.playAnimation(8, this.Transform);
                else idleAni.playAnimation(10, this.Transform);
            }
            else
            {
                jumpAni.playAnimation(15, this.Transform);
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
            if (isHurt)
            {
                if (hurtAni.isPlaying)
                {
                    hurtAni.PlayAnimationOnce(30, this.Transform);
                }
                else
                {
                    isHurt = false;
                    runAni.StartAnimation();
                    idleAni.StartAnimation();
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
            }

            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody other)
        {
            if (other.Parent.checkTag("Platform"))
            {
                jumpUp = false;
                inTheAir = false;
                stopForce = true;
            }
            if (other.Parent.checkTag("Bush"))
            {
                jumpUp = false;
                inTheAir = false;
                stopForce = true;
                health--;
                runAni.StopAnimation();
                idleAni.StopAnimation();
                hurtAni.StartAnimation();
                sound1Channel = sm.playSound(hurt, 1f);
                isHurt = true;
            }
        }

        public void onCollisionExit(PhysicsBody other)
        {
            if (other == null) return;
            if (other.Parent.checkTag("Platform"))
            {
                stopForce = false;
            }

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
