using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{ 
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));

            Instance.StartCoroutine(LoadBootstrapper());
        }

        static IEnumerator LoadBootstrapper()
        {
            while (!SceneManager.LoadSceneAsync(0, LoadSceneMode.Single).isDone)
            {
                yield return null;
            }
        }
    }
}

