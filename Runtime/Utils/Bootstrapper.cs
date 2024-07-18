using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{ 
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init(bool inDevelopment = true)
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            if (!inDevelopment)
                await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

