 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.GameManager
{
    public class LogController
    {
        public GameManager gameManager { get; private set; }

        public Queue<string> logs { get; private set; }

        private string ColoredString(string str, Color color)
        {
            return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), str);
        }

        private string LogString(string str, float timeSinceGameStart)
        {
            float time = Time.time - timeSinceGameStart;
            return string.Format("[{0}:{1:00}] {2}", (int)(time / 60), (int)(time % 60), str);
        }

        private string LogString(string str, float timeSinceGameStart, Color color)
        {
            return ColoredString(LogString(str, timeSinceGameStart), color);
        }

        public void AddLog(string str, float timeSinceGameStart)
        {
            logs.Enqueue(LogString(str, timeSinceGameStart));
        }

        public void AddLog(string str, float timeSinceGameStart, Color color)
        {
            logs.Enqueue(LogString(str, timeSinceGameStart, color));
        }

        public LogController(GameManager gameManager)
        {
            this.logs = new Queue<string>();
            this.gameManager = gameManager;
        }
    }
}