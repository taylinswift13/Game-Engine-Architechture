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
        public override void update()
        {
        }

        public override void initialize()
        {
            Player p = new Player();
            p.Transform.X = 50;
            p.Transform.Y = 50;
        }

        public override int getTargetFrameRate()
        {

            return 60;


        }
    }



}
