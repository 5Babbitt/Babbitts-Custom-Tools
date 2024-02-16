using Babbitt.Tools.Editors;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Babbitt.Tools.Editor
{
    public class CustomInputSetupWizard : ScriptableWizard
    {
        string packageInputPath = "Packages/com.5babbittgames.babbitts-custom-tools/Input";
        string assetInputPath = "Assets/Input";

        [MenuItem("Edit/Create Input Assets...")]
        static void CreateWizard()
        {
            DisplayWizard("Create Input Assets", typeof(CustomInputSetupWizard), "Create");
        }

        private void OnWizardCreate()
        {
            FileUtil.MoveFileOrDirectory(packageInputPath, assetInputPath);
            FileUtil.DeleteFileOrDirectory(packageInputPath);

            AssetDatabase.Refresh();
        }

        private void OnWizardUpdate()
        {
            
        }

        // Moves the existing Input Actions Asset to the created Input folder
        void InitializeInputFolder(string path)
        {
            /*AssetDatabase.CopyAsset(packageInputPath + "/GameInput.inputactions", path + "/GameInput.inputactions");
            AssetDatabase.CopyAsset(packageInputPath + "/InputManager.cs", path + "/InputManager.cs");
            AssetDatabase.CopyAsset(packageInputPath + "/InputReader.cs", path + "/InputReader.cs");
            AssetDatabase.CopyAsset("Packages/com.5babbittgames.babbitts-custom-tools/Editor/InputReaderEditor.cs", path + "/Editor/InputReaderEditor.cs");*/

            FileUtil.ReplaceDirectory(packageInputPath, path);
            FileUtil.DeleteFileOrDirectory(packageInputPath);

            AssetDatabase.CreateFolder("Assets/Input", "Editor");
            FileUtil.MoveFileOrDirectory("Packages/com.5babbittgames.babbitts-custom-tools/Editor/InputReaderEditor.cs", path + "/Editor/InputReaderEditor.cs");

            AssetDatabase.Refresh();
        }
    }
}
