using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TryAgain.GameElements.Managers
{
    class TimeMGR
    {
        private static List<Tuple<String, int, DateTime, int, myDelegate>>[] events = { new List<Tuple<string, int, DateTime, int, myDelegate>>(), new List<Tuple<string, int, DateTime, int, myDelegate>>() };
        private static int selectedList = 0;
        public delegate void myDelegate();
        private static DateTime lastUpdate = DateTime.MinValue;
        public static void Update()
        {
            int newList;
            if (selectedList == 0)
                newList = 1;
            else
                newList = 0;
            DateTime now = DateTime.Now;
            Tuple<String, int, DateTime, int, myDelegate> newEvent;

            for (int i = 0; i < events[selectedList].Count; i++)
            {
                //if ((events[selectedList][i].Item3.Second > now.Second) || ((events[selectedList][i].Item3.Second >= now.Second) && (events[selectedList][i].Item3.Millisecond > now.Millisecond)))
                if(events[selectedList][i].Item3.CompareTo(now) < 0)
                {

                    newEvent = new Tuple<string, int, DateTime, int, myDelegate>(
                        events[selectedList][i].Item1, events[selectedList][i].Item2, now.AddMilliseconds(events[selectedList][i].Item2), events[selectedList][i].Item4, events[selectedList][i].Item5);

                    if (events[selectedList][i].Item4 > 0)
                        newEvent = new Tuple<string, int, DateTime, int, myDelegate>(
                            events[selectedList][i].Item1, events[selectedList][i].Item2, newEvent.Item3, events[selectedList][i].Item4 - 1, events[selectedList][i].Item5);

                    events[selectedList][i].Item5();

                    if (events[selectedList][i].Item4 != 0)
                        events[newList].Add(newEvent);
                }
                else
                    events[newList].Add(events[selectedList][i]);

            }
            events[selectedList].RemoveAll((x) => true);
            selectedList = newList;
        }

        public static void AddEvent(String name, int milliseconds, int times, myDelegate function)
        {
            events[selectedList].Add(new Tuple<string, int, DateTime, int, myDelegate>(name, milliseconds, DateTime.Now, times, function));
        }

        public static void RemoveEvent(Predicate<string> predicate)
        {
            events[selectedList].RemoveAll((x) => (predicate(x.Item1)));
        }
    }
}
