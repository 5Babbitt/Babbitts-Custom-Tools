using UnityEngine;

namespace FiveBabbittGames
{
    [CreateAssetMenu(menuName="Settings/BootstrapperSettings")]
    public class BootstrapperSettings : ScriptableObject
    {
        public bool runFromBootsrapperScene = false; // change this value when building the project
    }
}

