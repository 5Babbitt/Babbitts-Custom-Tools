using System.IO;
using UnityEditor;

namespace FiveBabbittGames.Editors
{
    public static class CreateScriptTemplates
    {
        static string packagePath = "Packages/com.5babbittgames.babbitts-custom-tools/Editor/Templates/";
        static string assetPath = "Assets/_Scripts/Templates/";

        static bool CheckIfOverridePathExists(string templateName)
        {
            string filePath = assetPath + templateName;

            if (File.Exists(filePath))
            {
                return true;
            }
            
            return false;
        }

        static void CreateTemplateMenuItem(string templateName, string newScriptName)
        {
            string templatePath;

            if (CheckIfOverridePathExists(assetPath + templateName))
                templatePath = assetPath + templateName;
            else 
                templatePath = packagePath + templateName;

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, newScriptName);
        }

        // Basic Monobehaviour
        [MenuItem("Assets/Create/Code/MonoBehaviour", priority = 10)]
        public static void CreateMonobehaviourMenuItem()
        {
            string templateName = "Monobehaviour.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewMonobehaviourScript.cs");
        }

        // Singleton Class
        [MenuItem("Assets/Create/Code/Singleton", priority = 10)]
        public static void CreateSingletonMenuItem()
        {
            string templateName = "Singleton.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewSingletonScript.cs");
        }

        // ScriptableObject Class
        [MenuItem("Assets/Create/Code/ScriptableObject", priority = 10)]
        public static void CreateScriptableObjectMenuItem()
        {
            string templateName = "ScriptableObject.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewScriptableObject.cs");
        }

        // Editor Class
        [MenuItem("Assets/Create/Code/Editor", priority = 10)]
        public static void CreateEditorMenuItem()
        {
            string templateName = "Editor.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewEditorScript.cs");
        }

        // Basic C# Class
        [MenuItem("Assets/Create/Code/Class", priority = 10)]
        public static void CreatClassMenuItem()
        {
            string templateName = "Class.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewC#Class.cs");
        }

        // Interface
        [MenuItem("Assets/Create/Code/Interface", priority = 10)]
        public static void CreatInterfaceMenuItem()
        {
            string templateName = "Interface.cs.txt";
            
            CreateTemplateMenuItem(templateName, "NewInterface.cs");
        }
    }
}

