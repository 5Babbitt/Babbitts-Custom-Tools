using System.Collections.Generic;
using System.IO;
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
                UpdateGroupIndex(sceneLoader.SceneGroups);
            }

            base.OnInspectorGUI();
        }

        [ContextMenu("Update Groups Index")]
        public void UpdateGroupIndex(SceneGroup[] sceneGroups)
        {
            List<string> enumStrings = new();

            for (int i = 0; i < sceneGroups.Length; i++)
            {
                enumStrings.Add(sceneGroups[i].GroupName);
            }

            GenerateEnum("ESceneGroupIndex", enumStrings.ToArray());
            Debug.Log("Scene Group Index Updated");
        }

        public void GenerateEnum(string enumName, string[] names)
        {
            string folderPath = $"Assets/_Scripts/Enums/";
            string fullPath = folderPath + $"{enumName}.cs";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using (StreamWriter streamWriter = new StreamWriter(fullPath))
            {
                streamWriter.Write($"public enum {enumName}\n");
                streamWriter.Write("{\n");

                for (int i = 0; i < names.Length; i++)
                {
                    var enumString = names[i];

                    streamWriter.Write($"\t{enumString.Replace(" ", "_")} = {i},\n");
                }

                streamWriter.Write("}\n");
            }

            AssetDatabase.Refresh();
        }
    }
}
