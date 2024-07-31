using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        static BootstrapperSettings settings;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            //await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
            //settings = Resources.FindObjectsOfTypeAll<BootstrapperSettings>()[0];

            //if (settings.runFromBootsrapperScene) // Create Asset in the Resources Directory if this throws and error
        }
    }
}

