using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        static BootstrapperSettings settings;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public async static void Init()
        {
            settings = Resources.Load("BootstrapperSettings") as BootstrapperSettings;
            
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<BootstrapperSettings>();
            }

            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            if (settings.runFromBootsrapperScene) // Create Asset in the Resources Directory if this throws and error
                await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

