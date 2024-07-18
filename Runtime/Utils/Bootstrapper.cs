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

            if (settings.runFromBootsrapperScene) // Create Asset in the Resources Directory if this throws and error
                await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

