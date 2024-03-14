using UnityEngine;


namespace FiveBabbittGames
{
    // A static instance is similar to a singleton but does not destroy any new instances,
    // Instead overwriting the current existing instance.
    // Good for resetting states.
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(this);
        }
    }

    // A Basic Singleton. Will destroy any new instances created.
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null && Instance != this) 
            {
                Destroy(this);
                return;
            }

            base.Awake();
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