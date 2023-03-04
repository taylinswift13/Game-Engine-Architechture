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

            Platform tile1 = new(0, 394, 1);
            tile1.initialize();

            Platform tile2 = new(100, 25, 2);
            tile2.initialize();

            Platform tile3 = new(350, 294, 3);
            tile3.initialize();

            Platform tile4 = new(450, 394, 4);
            tile4.initialize();

            Platform tile5 = new(750, 25, 2);
            tile5.initialize();

            Platform tile6 = new(900, 444, 5);
            tile6.initialize();

            Platform tile7 = new(1300, 75, 6);
            tile7.initialize();

            Platform tile8 = new(1500, 25, 6);
            tile8.initialize();

            Platform tile9 = new(1784, 344, 7);
            tile9.initialize();

            //Enemy (36 x 24)
            Bush bush = new(264, 75, true, 0.5f, 75);
            Bush bush_1 = new(914, 75, true, 0.5f, 75);
            Bush bush_2 = new(900, 420, false, 0.5f, 75);

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
