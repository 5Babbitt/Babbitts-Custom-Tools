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
        [SerializeField] bool runOnStart = false;

        [Header("Scene Settings")]
        [SerializeField] SceneGroup[] sceneGroups;
        public SceneGroup[] SceneGroups => sceneGroups;

        float targetProgress;
        bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        protected override void Awake()
        {
            base.Awake();
            
            manager.OnSceneLoaded += sceneName => Debug.Log($"Loaded: {sceneName}");
            manager.OnSceneUnloaded += sceneName => Debug.Log($"Unloaded: {sceneName}");
            manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
        }

        async void Start()
        {
            EnableLoadingCanvas(false);
            
            if (runOnStart)
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
        /// <param name="index">The index of the loaded scene group</param>
        /// <param name="unloadActiveScene">If the active scene should be unloaded</param>
        /// <returns></returns>
        public async Task LoadSceneGroup(int index, bool unloadActiveScene = false)
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

        void EnableLoadingCanvas(bool enable = true)
        {
            isLoading = enable;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
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
