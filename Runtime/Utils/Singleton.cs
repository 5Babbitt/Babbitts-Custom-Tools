using UnityEngine;

namespace FiveBabbittGames
{
    // A Basic Singleton. Will destroy any new instances created.
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public abstract class PersistantSingleton<T> : Singleton<T> where T : Component
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}