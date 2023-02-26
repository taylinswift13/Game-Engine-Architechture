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
        Random rand;
        GameObject background;
        //GameObject top, left, right, bottom;
        //Random rand;

        //SFX stuff
        SoundManager sm = new SoundManager();
        int sound1Channel, sound2Channel;
        SoundStatus sound1Status, sound2Status;
        string BGM = "sparks fly.wav";
        string fire = "fire.wav";
        List<Platform> platform;

        public override void initialize()
        {
            sm.initializeAudioSystem();
            // sound1Channel = sm.playSound(BGM, 0.5f, true);
            //sound2Channel = sm.playSound(fire, 0.7f, false);

            background = new GameObject();
            background.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("backdrop.jpg");
            background.addTag("bg");


            rand = new Random();
            platform = new List<Platform>();
            Platform grass1 = new Platform(0, 150, 3);
            Platform grass2 = new Platform(250, 150, 2);
            Platform grass3 = new Platform(80, 0, 1);
            Platform grass4 = new Platform(100, 100, 1);
            for (int i = 0; i < 10; i++)
            {
                platform.Add(new Platform(rand.Next(0, 10) * 100, rand.Next(0, 10) * 100, rand.Next(1, 3)));
            }
            Bush bush = new Bush(600, 402);
            player = new Player();
            camera = new Camera()
            {
                Size = new Vector2(Bootstrap.getDisplay().getWidth(), Bootstrap.getDisplay().getHeight())
            };
        }

        public override void update()
        {
            background.Transform.X = Bootstrap.camPos.X;
            background.Transform.Y = Bootstrap.camPos.Y;
            Bootstrap.getDisplay().addToDraw(background);

            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //                                 Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);

            //sound1Status = sm.getSoundStatus(sound1Channel);
            //Console.WriteLine("Sound 1 status: " + sound1Status);

            //sound2Status = sm.getSoundStatus(sound2Channel);
            //Console.WriteLine("Sound 2 status: " + sound2Status);

            camera.FollowGameObject(Bootstrap.playerPos, 0.1f);
            //Console.WriteLine("camera: " + camera.Position.X + " " + camera.Position.Y);
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
