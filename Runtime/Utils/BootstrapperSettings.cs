using UnityEngine;

namespace FiveBabbittGames
{
    [CreateAssetMenu(fileName = "BootstrapperSettings", menuName = "Settings/BootstrapperSettings")]
    public class BootstrapperSettings : ScriptableObject
    {
        public bool runFromBootsrapperScene = false; // change this value when building the project
    }
}

