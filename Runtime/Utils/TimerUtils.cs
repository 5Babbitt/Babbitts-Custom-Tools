using UnityEngine;

namespace FiveBabbittGames
{
    public class TimerUtils
    {
        /// <summary>
        /// Converts the float value of time to minutes and seconds and returns it as a string
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FloatToTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
        
            string levelTime = minutes.ToString("00") + ":" + seconds.ToString("00");

            return levelTime;
        }

        /// <summary>
        /// Converts the float value of time to minutes, seconds and milliseconds and returns it as a string
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FloatToExactTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        
            string levelTime = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");

            return levelTime;
        }
    }

}
