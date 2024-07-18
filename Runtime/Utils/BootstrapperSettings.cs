using UnityEngine;

namespace FiveBabbittGames
{
    [CreateAssetMenu(menuName="Settings/BootstrapperSettings", fileName = "BootstrapperSettings")]
    public class BootstrapperSettings : ScriptableObject
    {
        public bool runFromBootsrapperScene = false; // change this value when building the project
    }
}

