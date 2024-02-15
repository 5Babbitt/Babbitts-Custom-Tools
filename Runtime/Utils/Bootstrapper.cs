using UnityEngine;

namespace Babbitt.Tools
{ 
    public class Bootstrapper : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load("Systems")));
        }
    }
}

