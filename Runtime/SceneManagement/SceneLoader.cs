using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

namespace FiveBabbittGames
{
    /// <summary>
    /// You add scene groups in the inspector and the scene loader handles loading and unloading the groups.
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] Image loadingBar;
        [SerializeField] float fillSpeed = 0.5f;
        [SerializeField] Canvas loadingCanvas;
        [SerializeField] Camera loadingCamera;

        [Header("Scene Settings")]
        [SerializeField] bool unloadActiveScene = true;
        [SerializeField] SceneGroup[] sceneGroups;

        float targetProgress;
        bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();
        public static readonly Dictionary<string, int> SceneGroupIndex = new();

        protected override void Awake()
        {
            base.Awake();
            
            manager.OnSceneLoaded += sceneName => Debug.Log($"Loaded: {sceneName}");
            manager.OnSceneUnloaded += sceneName => Debug.Log($"Unloaded: {sceneName}");
            manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");

            UpdateGroupIndex();
        }

        async void Start()
        {
            await LoadSceneGroup(0);
        }

        private void Update()
        {
            if (!isLoading) 
                return;

            float currentFillAmount = loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

            float dynamicFillSpeed = progressDifference * fillSpeed;

            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }
        
        /// <summary>
        /// Load A scene group using the index in the sceneLoader
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task LoadSceneGroup(int index)
        {
            loadingBar.fillAmount = 0;
            targetProgress = 1f;

            if (index < 0 || index >= sceneGroups.Length)
            {
                Debug.LogError($"Invalid scene group index: {index}");
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);

            EnableLoadingCanvas();
            await manager.LoadScenes(sceneGroups[index], progress, unloadActiveScene);
            EnableLoadingCanvas(false);
        }

        /// <summary>
        /// This is technically redundant with the ability to bake enums for the scene group names but I'll leave it just in case.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task LoadSceneGroup(string groupName)
        {
            await LoadSceneGroup(SceneGroupIndex[groupName]);
        }

        void EnableLoadingCanvas(bool enable = true)
        {
            isLoading = enable;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
        }

        public void SetUnloadActiveScene(bool value)
        {
            unloadActiveScene = value;
        }

        [ContextMenu("Update Groups Index")]
        public void UpdateGroupIndex()
        {
            List<string> enumStrings = new();

            for (int i = 0; i < sceneGroups.Length; i++)
            {
                enumStrings.Add(sceneGroups[i].GroupName);
            }

            GenerateEnum("ESceneGroupIndex", enumStrings.ToArray());
            Debug.Log("Scene Group Index Updated");
        }

        public void GenerateEnum(string enumName, string[] names)
        {
            string folderPath = $"Assets/_Scripts/Enums/";
            string fullPath = folderPath + $"{enumName}.cs";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            using (StreamWriter streamWriter = new StreamWriter(fullPath))
            {
                streamWriter.Write($"public enum {enumName}\n");
                streamWriter.Write("{\n");

                for (int i = 0; i < names.Length; i++)
                {
                    var enumString = names[i];

                    streamWriter.Write($"\t{enumString.Replace(" ", "_")} = {i},\n");
                }

                streamWriter.Write("}\n");
            }

            AssetDatabase.Refresh();
        }
    }

    public class LoadingProgress : IProgress<float> 
    { 
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value)
        {
            Progressed?.Invoke(value / ratio);
        }
    }
}
