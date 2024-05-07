using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace FiveBabbittGames
{
    /// <summary>
    /// SceneGroup
    /// </summary>
    [Serializable]
    public class SceneGroup
    {
        public string GroupName = "New Scene Group";
        public List<SceneData> Scenes;

        public string FindSceneNameByType(SceneType sceneType)
        {
            return Scenes.FirstOrDefault(scene => scene.SceneType == sceneType)?.Reference.Name;
        }
    }

    /// <summary>
    /// Scene Data
    /// </summary>
    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name;
        public SceneType SceneType;
    }

    public enum SceneType
    {
        ActiveScene,
        MainMenu,
        UI,
        HUD,
        Environment
    }
}
