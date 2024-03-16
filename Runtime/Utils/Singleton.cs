using UnityEngine;

namespace FiveBabbittGames
{
    // A Basic Singleton. Will destroy any new instances created.
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this as T;

            if (Instance != null && Instance != this) 
            {
                Destroy(gameObject);
                return;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }

    }

    // Persistant version of a singleton. Will not be destroyed when loading a new scene.
    // Ideal for systems that require persistant data or operating while scene loading.
    public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}