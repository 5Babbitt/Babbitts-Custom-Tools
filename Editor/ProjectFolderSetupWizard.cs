using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

namespace Babbitt.Tools.Editors
{
    // This script has been extended by Babbitt for personal use if you manage to get your hands on this and want the original it can be found using the link below
    // https://forum.unity.com/threads/project-folder-auto-creation-script-c.268367/

    /*
    Developed by Tommy Vogt Sept. 2014
    The MIT License (MIT)
 
    Copyright (c) <2014> <Tommy Vogt>
 
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
 
    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.
 
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 
 
    Directory creation script for quick creation of directories in a new Unity3D or 2D project...
    Use :
    1. Click Edit -> Create Project Folders * as long as there are no build errors, you will see a new menu item near the bootom of the Edit menu
    2. If you are using Namespaces, clicking the checkbox will include three basic namespce folders
    3. Right clicking on a list item will let you delete the item, if you want
    4. Increasing the List size will add another item with the prior items name, click in the space to rename.
    5. Clicking "Create" will create all the files listed, the Namespace folders will be added to the script directory.
    */

    public class ProjectFolderSetupWizard : ScriptableWizard
    {
        string packageInputPath = "Packages/com.5babbittgames.babbitts-custom-tools/Runtime/Input";

        public List<string> folders = new List<string>() { "Scenes", "_Scripts", "Animation", "Audio", "Materials", "Meshes", "Prefabs", "Resources", "Textures", "Sprites", "Input", "GameEvents" };
       
        [MenuItem("Edit/Create Project Folders...")]
        static void CreateWizard()
        {
            DisplayWizard("Create Project Folders", typeof(ProjectFolderSetupWizard), "Create");
        }

        //Create button click
        void OnWizardCreate()
        {
            //create all the folders required in a project
            foreach (string folder in folders)
            {
                if (folder == "Input")
                {
                    InitializeInputFolder("Assets/Input");
                    return;
                }

                string guid = AssetDatabase.CreateFolder("Assets", folder);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                if (folder == "Resources")
                    InitializeResourcesFolder(newFolderPath);
            }

            AssetDatabase.Refresh();
        }

        //Runs whenever something changes in the editor window...
        void OnWizardUpdate()
        {
            
        }

        // Add the Systems prefab, which is used by the Bootstrapper, to the Resources Folder
        void InitializeResourcesFolder(string path)
        {
            GameObject systemsPrefab = new GameObject("Systems");
            path = AssetDatabase.GenerateUniqueAssetPath(path + $"/{systemsPrefab.name}.prefab");

            PrefabUtility.SaveAsPrefabAsset(systemsPrefab, path);

            DestroyImmediate(systemsPrefab);
        }

        // Moves the existing Input Actions Asset to the created Input folder
        void InitializeInputFolder(string path)
        {
            /*AssetDatabase.CopyAsset(packageInputPath + "/GameInput.inputactions", path + "/GameInput.inputactions");
            AssetDatabase.CopyAsset(packageInputPath + "/InputManager.cs", path + "/InputManager.cs");
            AssetDatabase.CopyAsset(packageInputPath + "/InputReader.cs", path + "/InputReader.cs");
            AssetDatabase.CopyAsset("Packages/com.5babbittgames.babbitts-custom-tools/Editor/InputReaderEditor.cs", path + "/Editor/InputReaderEditor.cs");*/
            FileUtil.MoveFileOrDirectory(packageInputPath, path);
            FileUtil.MoveFileOrDirectory("Packages/com.5babbittgames.babbitts-custom-tools/Editor/InputReaderEditor.cs", path + "/Editor");

            //FileUtil.DeleteFileOrDirectory(packageInputPath);

            AssetDatabase.Refresh();
        }
    }
}
