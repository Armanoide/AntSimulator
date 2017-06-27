using LibAbstract.ManageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageEnvironment
{
    public class Sun : AbstractEnvironment
    {
        public int Day { get; private set; }
        public bool IsNight { get; private set; }
        public double LastTimeNight { get; set; }
        private List<IObserverSun> _listObserver { get; set; }
        private static Sun _instance = null;
        
        public static Sun Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Sun();
                }
                return _instance;
            }
        }



        private Sun()
        {
            Reset();
        }

        public void Reset()
        {
            Day = 0;
            IsNight = false;
            LastTimeNight = -5000;
            _listObserver = new List<IObserverSun>();
        }

        public void AddObserver(IObserverSun observer)
        {
            _listObserver.Add(observer);
            if (IsNight)
            {
                observer.NotifyIsNight();
            }
            else
            {
                observer.NotifyIsDay();
            }
        }

        public void RemoveObserver(IObserverSun observer)
        {
            _listObserver.Remove(observer);
        }

        private void DispatchUpdateToObserver()
        {
            foreach (IObserverSun obs in _listObserver)
            {
                if (IsNight)
                {
                    obs.NotifyIsNight();
                }
                else
                {
                    obs.NotifyIsDay();
                }
            }
        }

        public void UpdateTime(double totalTime)
        {
            // 5 min
            if (!IsNight && totalTime - LastTimeNight >= /*300000*/ 60000/2)
            {            
                IsNight = true;
                LastTimeNight = totalTime;
                DispatchUpdateToObserver();
            }

            if (IsNight && totalTime - LastTimeNight >= 60000/2)
            {
                Day++;
                IsNight = false;
                LastTimeNight = totalTime;
                DispatchUpdateToObserver();
            }
        }


    }
}
