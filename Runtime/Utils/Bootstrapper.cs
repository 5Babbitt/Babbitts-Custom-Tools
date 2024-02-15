using UnityEngine;

namespace Babbitt.Tools.Utils
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

