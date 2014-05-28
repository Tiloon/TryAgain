using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    public struct Stats
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
        public static Stats operator +(Stats s1, Stats s2)
        {
            Stats stats;
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
}
