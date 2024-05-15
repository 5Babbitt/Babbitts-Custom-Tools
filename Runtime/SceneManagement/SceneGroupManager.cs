using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{
    /// <summary>
    /// Handles loading the individual scenes in a scene group based on their type and status.
    /// </summary>
    public class SceneGroupManager
    {
        public event Action<string> OnSceneLoaded = delegate { };
        public event Action<string> OnSceneUnloaded = delegate { };
        public event Action OnSceneGroupLoaded = delegate { };

        SceneGroup ActiveSceneGroup;
        private bool unloadActiveScene;

        public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool unloadActiveScene, bool reloadDupScenes = false)
        {
            ActiveSceneGroup = group;
            var loadedScenes = new List<string>();

            await UnloadScenes(unloadActiveScene);

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            var totalScenesToLoad = ActiveSceneGroup.Scenes.Count;

            var operationGroup = new AsyncOperationGroup(totalScenesToLoad);

            for (int i = 0; i < totalScenesToLoad; i++)
            {
                var sceneData = group.Scenes[i];

                if (!reloadDupScenes && loadedScenes.Contains(sceneData.Name)) 
                    continue;

                var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));

                operationGroup.Operations.Add(operation);

                OnSceneLoaded.Invoke(sceneData.Name);
            }

            while (!operationGroup.IsDone)
            {
                progress?.Report(operationGroup.Progress);
                await Task.Delay(100);
            }

            Scene activeScene = SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

            if (activeScene.IsValid())
                SceneManager.SetActiveScene(activeScene);

            OnSceneGroupLoaded.Invoke();
        }

        public async Task UnloadScenes(bool unloadActiveScene = false)
        {
            var scenes = new List<string>();

            if (unloadActiveScene)
                SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
                
            var activeScene = SceneManager.GetActiveScene().name;

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                var sceneAt = SceneManager.GetSceneAt(i);

                if (!sceneAt.isLoaded) 
                    continue;    

                var sceneName = sceneAt.name;


                if (sceneName.Equals(activeScene) || sceneName == "Bootstrapper")
                    continue;

                scenes.Add(sceneName);
            }

            var operationGroup = new AsyncOperationGroup(scenes.Count);

            foreach (var scene in scenes)
            {
                var operation = SceneManager.UnloadSceneAsync(scene);
                
                if (operation == null)
                    continue;

                operationGroup.Operations.Add(operation);

                OnSceneUnloaded.Invoke(scene);
            }

            while (!operationGroup.IsDone)
            {
                await Task.Delay(100);
            }
        }
    }

    public readonly struct AsyncOperationGroup
    {
        public readonly List<AsyncOperation> Operations;

        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(operation => operation.progress);
        public bool IsDone => Operations.All(operation => operation.isDone);

        public AsyncOperationGroup(int initialCapacity)
        {
            Operations = new List<AsyncOperation>(initialCapacity);
        }
    }

}
