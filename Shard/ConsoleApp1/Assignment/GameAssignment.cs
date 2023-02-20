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

        public static Player player = new Player();
        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //                                 Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
        }

        public override void initialize()
        {
            player.Transform.X = 50;
            player.Transform.Y = 50;
            Bootstrap.getSound().initializeAudioSystem();
            Bootstrap.getSound().playSound("fire.wav", 0.1f, true);
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
