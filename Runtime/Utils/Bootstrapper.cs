using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{ 
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}

