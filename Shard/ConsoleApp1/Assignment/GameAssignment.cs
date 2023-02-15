using GameAssignment;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shard
{
    class GameAssignment : Game
    {
        GameObject top, left, right, bottom;
        Random rand;
        public static Player player = new Player();
        public override void update()
        {
        }

        public override void initialize()
        {
            player.Transform.X = 50;
            player.Transform.Y = 50;
        }

        public override int getTargetFrameRate()
        {

            return 60;


        }
    }



}
