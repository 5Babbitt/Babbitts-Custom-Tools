using UnityEditor;
using UnityEngine;

namespace FiveBabbittGames.Editors
{
    [CustomEditor(typeof(SceneLoader))]
    public class SceneLoaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SceneLoader sceneLoader = (SceneLoader)target;
            if (GUILayout.Button("Update Group Index"))
            {
                sceneLoader.UpdateGroupIndex();
            }

            base.OnInspectorGUI();
        }
    }
}
