﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace RoomsOfDoom
{
    public class Enemy : IHittable, ITile
    {
        
        public string name;
        protected int maxHP;
        protected int currentHP;
        protected bool alive;
        protected char glyph;

        public Enemy(string name, char glyph, int hp)
        {
            this.name = name;
            myPack = null;
            maxHP = hp;
            currentHP = hp;
            alive = true;
            this.glyph = glyph;
        }

        public bool Hit(int damage)
        {
            CurrentHP -= damage;
            return !Alive;
        }

        public Pack myPack
        {
            get;
            set;
        }

        public int CurrentHP
        {
            get
            {
                return currentHP;
            }
            set
            {
                currentHP = value;
                if (currentHP <= 0)
                {
                    myPack.Enemies.Remove(this);
                    alive = false;
                }
                if (currentHP > MaxHP)
                    currentHP = MaxHP;
            }
        }

        public int MaxHP
        {
            get{return maxHP;}
            set { maxHP = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public char Glyph
        {
            get { return glyph; }
        }

        public Point Location
        {
        get;
        set;
        }

        // TODO: Enemies still walk through the player when not targeting the player
        public virtual bool NeutralMove(Point p)
        {
            int x = p.X - Location.X;
            int y = p.Y - Location.Y;
            Point loc = Math.Abs(x) > Math.Abs(y) ?
                new Point(Location.X + Math.Sign(x), Location.Y) :
                new Point(Location.X, Location.Y + Math.Sign(y));

            foreach (Enemy teammate in myPack)
            {
                if (teammate.Location == loc)
                {
                    loc = Math.Abs(x) <= Math.Abs(y) ?
                        new Point(Location.X + Math.Sign(x), Location.Y) :
                        new Point(Location.X, Location.Y + Math.Sign(y));
                    foreach (Enemy teamy in myPack)
                    {
                        if (teamy.Location == loc)
                            return false;
                    }
                    break;
                }
            }

            if (loc == p)
            {
                // Insert logic for gates here
            }
            Location = loc;
            return true;
        }

        public virtual bool AggressiveMove<T>(T target) where T : IHittable, ITile
        {
            int x = target.Location.X - Location.X;
            int y = target.Location.Y - Location.Y;
            Point loc = Math.Abs(x) > Math.Abs(y) ? 
                new Point(Location.X + Math.Sign(x), Location.Y) : 
                new Point(Location.X, Location.Y + Math.Sign(y));

            foreach(Enemy teammate in myPack)
            {
                if(teammate.Location == loc)
                {
                    loc = Math.Abs(x) <= Math.Abs(y) ?
                        new Point(Location.X + Math.Sign(x), Location.Y) :
                        new Point(Location.X, Location.Y + Math.Sign(y));
                    foreach (Enemy teamy in myPack)
                    {
                        if (teamy.Location == loc)
                            return false;
                    }
                    break;
                }
            }

            if (loc == target.Location)
            {
                KillTheHeretic(target);
                return true;
            }

            Location = loc;
            return true;
        }

        public void KillTheHeretic(IHittable p)
        {
            p.Hit(1);
        }
    }
}
