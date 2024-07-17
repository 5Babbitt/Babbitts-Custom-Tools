using System;
using UnityEngine;

namespace FiveBabbittGames
{
    public class TimerUtils
    {
        /// <summary>
        /// Converts the float value of time to minutes and seconds and returns it as a string
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A string value with minutes, seconds formatted as 00:00</returns>
        public static string FloatToTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            string levelTime = minutes.ToString("00") + ":" + seconds.ToString("00");

            return levelTime;
        }

        /// <summary>
        /// Converts the float value of time to minutes, seconds and milliseconds and returns it as a string
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A string value with minutes, seconds and milliseconds formatted as 00:00:000</returns>
        public static string FloatToExactTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

            string levelTime = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");

            return levelTime;
        }

        /// <summary>
        /// Converts the float value to seconds represented in integers
        /// </summary>
        /// <param name="time"></param>
        /// <returns>An interger value for seconds</returns>
        public static int FloatToSeconds(float time)
        {
            int seconds = Mathf.CeilToInt(time);

            return seconds;
        }
    }
}
