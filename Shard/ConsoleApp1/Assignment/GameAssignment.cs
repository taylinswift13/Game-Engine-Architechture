using GameAssignment;

using SDL2;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class GameAssignment : Game, InputListener
    {
        Camera camera;
        Player player;
        //GameObject top, left, right, bottom;
        //Random rand;

        //SFX stuff
        SoundManager sm = new SoundManager();
        int sound1Channel, sound2Channel;
        SoundStatus sound1Status, sound2Status;
        string BGM = "sparks fly.wav";
        string fire = "fire.wav";

        public override void initialize()
        {
            sm.initializeAudioSystem();
            sound1Channel = sm.playSound(BGM, 0.5f, true);
            //sound2Channel = sm.playSound(fire, 0.7f, false);
            
            Platform grass1 = new Platform(0, 500);
            Platform grass2 = new Platform(450, 450);
            Platform grass3 = new Platform(150, 200);
            player = new Player();
            camera = new Camera()
            {
                Size = new Vector2(Bootstrap.getDisplay().getWidth(), Bootstrap.getDisplay().getHeight())
            };
        }

        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //                                 Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);

            //sound1Status = sm.getSoundStatus(sound1Channel);
            //Console.WriteLine("Sound 1 status: " + sound1Status);

            //sound2Status = sm.getSoundStatus(sound2Channel);
            //Console.WriteLine("Sound 2 status: " + sound2Status);

            camera.FollowGameObject(Bootstrap.playerPos);
            Console.WriteLine("camera: " + camera.Position.X + " " + camera.Position.Y);
            //Console.WriteLine("player: " + player.Transform.X + " " + player.Transform.Y);
        }

        public override int getTargetFrameRate()
        {
            return 60;
        }

        public void handleInput(InputEvent inp, string eventType)
        {
        }
    }
}
