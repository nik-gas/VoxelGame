namespace MyTerraria
{
    public struct Clock
    {
        public int Minutes { get; set; }
        public int Hours { get; set; } 

        public Clock(int min, int hour)
        {
            Minutes = min;
            Hours = hour;
        }  
    }

    public class Time
    {
        private Clock clock;
        private float seconds;
        
        public bool isDown = false;

        public Time()
        {
            clock = new Clock();
        }

        public void UpdateClock(float sec)
        {
            seconds += sec;

            if(seconds >= 60)
            {
                clock.Minutes++;
                seconds = 0;

                if(clock.Minutes >= 60)
                {
                    clock.Minutes = 0;

                    if(!isDown)
                        clock.Hours++;

                    if(isDown)
                        clock.Hours--;

                    if(clock.Hours >= 24)
                        isDown = true;
                    if(clock.Hours == 0)
                        isDown = false;
                }
            }

        }

        public Clock GetClock()
        {
            return clock;
        }

    }
}