﻿using GameAssignment;

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
        BackgroundManager bgFront, bgBack;

        //SFX stuff
        SoundManager sm = new SoundManager();
        int sound1Channel, sound2Channel;
        SoundStatus sound1Status, sound2Status;
        string BGM = "sparks fly.wav";
        string fire = "fire.wav";
        List<Platform> platform;

        public override void initialize()
        {
            //Sound
            sm.initializeAudioSystem();
            // sound1Channel = sm.playSound(BGM, 0.5f, true);
            //sound2Channel = sm.playSound(fire, 0.7f, false);

            //Background
            bgBack = new BackgroundManager(new Vector2(992, 544));
            bgBack.AddBackground("Background_2.png", 0, 0, 1, 1);
            bgBack.AddBackground("Background_2.png", 992, 0, 1, 1);
            bgBack.AddBackground("Background_2.png", 1984, 0, 1, 1);

            bgFront = new BackgroundManager(new Vector2(992, 544));
            bgFront.AddBackground("Background_1.png", 0, 0, 1, 1);
            bgFront.AddBackground("Background_1.png", 992, 0, 1, 1);
            bgFront.AddBackground("Background_1.png", 1984, 0, 1, 1);


            //Platform and level
            rand = new Random();
            platform = new List<Platform>();
            Platform grass1 = new Platform(0, 150, 3);
            Platform grass2 = new Platform(350, 200, 2);
            Platform grass3 = new Platform(200, 0, 1);
            Platform grass4 = new Platform(100, 100, 1);
            for (int i = 0; i < 0; i++)
            {
                platform.Add(new Platform(rand.Next(0, 10) * 100, rand.Next(0, 10) * 100, rand.Next(1, 3)));
            }
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

            //sound2Status = sm.getSoundStatus(sound2Channel);
            //Console.WriteLine("Sound 2 status: " + sound2Status);

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
