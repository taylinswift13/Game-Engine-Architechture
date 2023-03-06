using GameAssignment;

using SDL2;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;

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
        List<Heart> hearts;
        Winpoint winPoint;

        //SFX
        SoundManager sm = new SoundManager();
        int sound1Channel;
        SoundStatus sound1Status;
        string BGM = "BGM2.wav";

        public override void initialize()
        {
            //Sound
            sm.initializeAudioSystem();
            sound1Channel = sm.playSound(BGM, 0.3f, true);

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
            hearts = new List<Heart>();

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
            hearts.Add(new Heart(50, 50));
            hearts.Add(new Heart(100, 50));
            hearts.Add(new Heart(150, 50));

            winPoint = new Winpoint(1900, 250);

        }

        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);

            //Backgrounds
            bgBack.Update(0.25f);
            bgFront.Update(0.5f);
            bgBack.Draw();
            bgFront.Draw();

            camera.FollowGameObject(Bootstrap.playerPos, 0.03f);

            if (player.Health < hearts.Count)
            {
                hearts[player.Health].ToBeDestroyed = true;
                hearts.RemoveAt(player.Health);
            }

            if (isRunning() == false)
            {
                if (hearts.Count == 0)
                {
                    Color col = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                    Bootstrap.getDisplay().showText("GAME OVER!", 150, 200, 128, col);
                    return;
                }
                else if (winPoint.Win == true)
                {
                    Color col = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                    Bootstrap.getDisplay().showText("YOU WIN!", 150, 200, 128, col);
                    return;
                }
            }
        }
        public override bool isRunning()
        {

            if (hearts.Count == 0)
            {
                return false;
            }
            if (winPoint.Win == true)
            {
                return false;
            }
            return true;

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
