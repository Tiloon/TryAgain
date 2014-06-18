using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TryAgain.Characters
{
    struct CharacterStats
    {
        public int lvl;
        public int lp; // Life points
        public int lpmax;
        public int mh; // Mental Health
        public int mhmax;
        public int ch; // Cafeine health
        public int chmax;
        public int cbonus; // cafeine bonus
        public int mp; // Magic points
        public int mpmax;
        public int force;
        public int intelligence;
        public int defense;
        public int criticalrate;
        public float speed;
        public static CharacterStats operator +(CharacterStats s1, CharacterStats s2)
        {
            CharacterStats stats;
            stats.lvl = s1.lvl + s2.lvl;
            stats.lp = s1.lp + s2.lp;
            stats.lpmax = s1.lpmax + s2.lpmax;
            stats.mh = s1.mh + s2.mh;
            stats.mhmax = s1.mhmax + s2.mhmax;
            stats.ch = s1.ch + s2.ch;
            stats.chmax = s1.chmax + s2.chmax;
            stats.cbonus = s1.cbonus + s2.cbonus;
            stats.mp = s1.mp + s2.mp;
            stats.mpmax = s1.mpmax + s2.mpmax;
            stats.force = s1.force + s2.force;
            stats.intelligence = s1.intelligence + s2.intelligence;
            stats.defense = s1.defense + s2.defense;
            stats.criticalrate = s1.criticalrate + s2.criticalrate;
            stats.speed = s1.speed + s2.speed;
            return stats;
        }
    }

    class Stats
    {
        public static CharacterStats GetStats(int lvl)
        {
            CharacterStats stats;
            if (lvl == 0)
            {
                stats.lvl = 1;
                stats.lp = 100;
                stats.lpmax = 100;
                stats.mh = 100;
                stats.mhmax = 100;
                stats.ch = 80;
                stats.chmax = 100;
                stats.cbonus = 0;
                stats.mp = 0;
                stats.mpmax = 0;
                stats.force = 10;
                stats.intelligence = 10;
                stats.defense = 10;
                stats.criticalrate = 0;
                stats.speed = 0.3F;
            }
            else
            {
                stats.lvl = 1;
                stats.lp = 5;
                stats.lpmax = 5;
                stats.mh = 5;
                stats.mhmax = 5;
                stats.ch = 0;
                stats.chmax = 2;
                stats.cbonus = 0;
                stats.mp = 5;
                stats.mpmax = 5;
                stats.force = 1;
                stats.intelligence = 1;
                stats.defense = 1;
                stats.criticalrate = 1;
                stats.speed = 0.3F;
            }
            return stats;
        }
    }
}
