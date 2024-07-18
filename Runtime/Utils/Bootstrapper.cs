using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{ 
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        static bool runFromBootsrapperScene = false; // change this value when building the project

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            if (runFromBootsrapperScene)
                await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

