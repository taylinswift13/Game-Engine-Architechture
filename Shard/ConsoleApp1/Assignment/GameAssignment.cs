using GameAssignment;

using SDL2;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class GameAssignment : Game, InputListener
    {
        //GameObject top, left, right, bottom;
        //Random rand;
        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //                                 Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
        }

        public override void initialize()
        {
            Bootstrap.getSound().initializeAudioSystem();
            Bootstrap.getSound().playSound("fire.wav", 0.1f, true);
            Player player = new Player();
            Platform grass1 = new Platform(0, 500);
            Platform grass2 = new Platform(600, 450);
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
