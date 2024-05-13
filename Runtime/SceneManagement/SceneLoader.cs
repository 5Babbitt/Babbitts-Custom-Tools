using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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

            UpdateGroupDictionary();
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
            await manager.LoadScenes(sceneGroups[index], progress);
            EnableLoadingCanvas(false);
        }

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

        [ContextMenu("Update Groups Index")]
        public void UpdateGroupDictionary()
        {
            SceneGroupIndex.Clear();

            for (int i = 0; i < sceneGroups.Length; i++)
            {
                SceneGroupIndex.Add(sceneGroups[i].GroupName, i);
            }

            Debug.Log("Dictionary updated");

            foreach (string key in  SceneGroupIndex.Keys)
            {
                Debug.Log($"{key} : {SceneGroupIndex[key]}");
            }
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
