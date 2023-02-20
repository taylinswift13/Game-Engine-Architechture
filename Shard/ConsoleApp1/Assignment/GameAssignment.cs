using GameAssignment;

using SDL2;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class GameAssignment : Game, InputListener
    {
        SoundManager sm = new SoundManager();
        int sound1Channel, sound2Channel;
        //GameObject top, left, right, bottom;
        //Random rand;
        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + 
            //                                 Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);

            //SoundStatus sound1Status = sm.getSoundStatus(sound1Channel);
            //Console.WriteLine("Sound 1 status: " + sound1Status);

            //SoundStatus sound2Status = sm.getSoundStatus(sound2Channel);
            //Console.WriteLine("Sound 2 status: " + sound2Status);
        }

        public override void initialize()
        {
            sm.initializeAudioSystem();
            sound1Channel = sm.playSound("sparks fly.wav", 0.5f, true);
            //sound2Channel = sm.playSound("fire.wav", 0.7f, false);
            Player player = new Player();
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
