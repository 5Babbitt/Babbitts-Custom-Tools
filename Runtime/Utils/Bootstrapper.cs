using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        static BootstrapperSettings settings = Resources.FindObjectsOfTypeAll<BootstrapperSettings>()[0];

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            if (settings.runFromBootsrapperScene)
                await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

