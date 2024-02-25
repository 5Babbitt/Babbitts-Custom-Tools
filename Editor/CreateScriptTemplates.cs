using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FiveBabbittGames.Editors
{
    public static class CreateScriptTemplates
    {
        static string packagePath = "Packages/com.5babbittgames.babbitts-custom-tools/Editor/Templates/";

        [MenuItem("Assets/Create/Code/MonoBehaviour", priority = 10)]
        public static void CreateMonobehaviourMenuItem()
        {
            string templatePath = packagePath + "Monobehaviour.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
        }

        [MenuItem("Assets/Create/Code/Singleton", priority = 10)]
        public static void CreateSingletonMenuItem()
        {
            string templatePath = "Assets/Tools-Development-Project/_Scripts/Templates/Singleton.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewSingletonScript.cs");
        }

        [MenuItem("Assets/Create/Code/ScriptableObject", priority = 10)]
        public static void CreateScriptableObjectMenuItem()
        {
            string templatePath = "Assets/Tools-Development-Project/_Scripts/Templates/ScriptableObject.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScriptableObject.cs");
        }

        [MenuItem("Assets/Create/Code/Editor", priority = 10)]
        public static void CreateEditorMenuItem()
        {
            string templatePath = "Assets/Tools-Development-Project/_Scripts/Templates/Editor.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewEditorScript.cs");
        }

        [MenuItem("Assets/Create/Code/Class", priority = 10)]
        public static void CreatClassMenuItem()
        {
            string templatePath = "Assets/Tools-Development-Project/_Scripts/Templates/Class.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewC#Class.cs");
        }

        [MenuItem("Assets/Create/Code/Interface", priority = 10)]
        public static void CreatInterfaceMenuItem()
        {
            string templatePath = "Assets/Tools-Development-Project/_Scripts/Templates/Interface.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewInterface.cs");
        }
    }
}
