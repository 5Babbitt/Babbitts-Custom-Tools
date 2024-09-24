using System.Collections.Generic;
using UnityEngine;

namespace FiveBabbittGames
{
    [CreateAssetMenu(menuName="GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> listeners = new List<GameEventListener>();

        public bool ShowDebug;

        public void Raise() 
        {
            Raise(null, null);
        }

        public void Raise(object data) 
        {
            Raise(null, data);
        }

        public void Raise(Component sender) 
        {
            Raise(sender, null);
        }

        public void Raise(bool data)
        {
            Raise(null, data);
        }

        public void Raise(string data)
        {
            Raise(null, data);
        }

        public void Raise(int data)
        {
            Raise(null, data);
        }

        public void Raise(float data)
        {
            Raise(null, data);
        }

        public void Raise(Component sender, object data) 
        {
            for (int i = 0; i < listeners.Count; i++) 
            {
                listeners[i].OnEventRaised(sender, data);
            }
        }
    
        public void RegisterListener(GameEventListener listener) 
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);

            if (ShowDebug)
            {
                Debug.Log($"Listener is of type {listener.GetType()}");
                Debug.Log(listener is GameEventListener);
            }
        }

        public void UnregisterListener(GameEventListener listener) 
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

    }
}

