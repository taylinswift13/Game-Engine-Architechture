using System;
using System.Numerics;
using Shard;

namespace GameAssignment
{
    class Winpoint : GameObject, InputListener, CollisionHandler
    {
        bool win;
        public bool Win { get => win; set => win = value; }

        SoundManager sm = new SoundManager();
        int sound1Channel;
        string winning = "laughter.wav";

        AnimationSystem animation;
        public Winpoint(float x, float y) // added physicsEnabled parameter with default value of true
        {
            Transform.X = x / Bootstrap.CamViewScale;
            Transform.Y = y / Bootstrap.CamViewScale;
        }

        public override void initialize()
        {
            win = false;
            sm.initializeAudioSystem();
            Transform.Scalex = 0.5f;
            Transform.Scaley = 0.5f;
            setPhysicsEnabled();
            MyBody.Mass = 1;
            MyBody.addRectCollider();
            MyBody.Kinematic = true;

            animation = new AnimationSystem();
            animation.loadAnimation("winpoint", 2);

        }

        public void handleInput(InputEvent inp, string eventType)
        {
        }

        public override void update()
        {
            animation.playAnimation(30, this.Transform);
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody other)
        {
            if (other.Parent.checkTag("Player"))
            {
                win = true;
                sound1Channel = sm.playSound(winning, 1f);
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
            return "Winpoint: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}


