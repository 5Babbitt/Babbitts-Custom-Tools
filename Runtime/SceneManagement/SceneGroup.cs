using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace FiveBabbittGames
{
    /// <summary>
    /// A List of different Scenes to be loaded additively.
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
    /// Contains the name and type of scene for use in the Scene Group.
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
        Environment,
        Audio
    }
}
