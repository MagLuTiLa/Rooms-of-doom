﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsOfDoom.Items
{
    class TimeCrystal : IItem
    {
        public TimeCrystal()
        {
            Duration = 2;
        }

        public void Use(Player player, Dungeon dungeon)
        {
            player.OP = true;
        }

        public void Finish(Player player)
        {
            player.OP = false;
        }

        public int Duration
        {
            get;
            set;
        }

        public int Id
        {
            get { return 1; }
        }
    }
}
