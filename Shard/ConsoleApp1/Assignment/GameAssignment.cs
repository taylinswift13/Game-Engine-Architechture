using GameAssignment;

using SDL2;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class GameAssignment : Game, InputListener
    {
        //Instance
        Camera camera;
        Player player;
        Random rand;
        BackgroundManager bgFront, bgBack;
        List<Platform> tile;

        //SFX
        SoundManager sm = new SoundManager();
        int sound1Channel;
        SoundStatus sound1Status;
        string BGM = "BGM.wav";

        public override void initialize()
        {
            //Sound
            sm.initializeAudioSystem();
            sound1Channel = sm.playSound(BGM, 0.5f, true);

            //Background
            bgBack = new BackgroundManager(new Vector2(992, 544));
            bgBack.AddBackground("Background_2.png", 0, 0, 1, 1);
            bgBack.AddBackground("Background_2.png", 992, 0, 1, 1);
            bgBack.AddBackground("Background_2.png", 1984, 0, 1, 1);

            bgFront = new BackgroundManager(new Vector2(992, 544));
            bgFront.AddBackground("Background_1.png", 0, 0, 1, 1);
            bgFront.AddBackground("Background_1.png", 992, 0, 1, 1);
            bgFront.AddBackground("Background_1.png", 1984, 0, 1, 1);

            //Level
            rand = new Random();
            tile = new List<Platform>();
            /*for (int i = 0; i < 0; i++)
            {
                platform.Add(new Platform(rand.Next(0, 10) * 100, rand.Next(0, 10) * 100, rand.Next(1, 3)));
            }*/

            Platform tile1 = new(0, 394, 1);
            tile1.initialize();

            Platform tile2 = new(175, 75, 2);
            tile2.initialize();

            //Enemy
            Bush bush = new Bush(450, 164);

            //Player and camera
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

            //Backgrounds
            bgBack.Update(0.25f);
            bgFront.Update(0.5f);
            bgBack.Draw();
            bgFront.Draw();

            //sound1Status = sm.getSoundStatus(sound1Channel);
            //Console.WriteLine("Sound 1 status: " + sound1Status);

            camera.FollowGameObject(Bootstrap.playerPos, 0.03f);
            Console.WriteLine("player: " + player.Transform.X + " " + player.Transform.Y);
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
