using Eflatun.SceneReference;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace FiveBabbittGames
{ 
    public static class SceneReferenceUtils
    {
        /// <summary>
        /// Checks if the scene is the same as the cached scene reference. 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static bool IsReferenced(this Scene scene, SceneReference reference)
        {
            if (scene.buildIndex == reference.BuildIndex)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the scene is in the cached array of scene references. 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static bool IsReferenced(this Scene scene, SceneReference[] reference)
        {
            foreach (SceneReference referenceItem in reference)
            {
                if (scene.buildIndex == referenceItem.BuildIndex)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the scene is in the cached list of scene references. 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static bool IsReferenced(this Scene scene, List<SceneReference> reference)
        {
            foreach (SceneReference referenceItem in reference)
            {
                if (scene.buildIndex == referenceItem.BuildIndex)
                    return true;
            }

            return false;
        }
    }
}