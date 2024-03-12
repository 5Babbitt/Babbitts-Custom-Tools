//
// Copyright (c) 2023 Warped Imagination. All rights reserved. 
//

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// Overlays were introduced in Unity Editor 2021 
#if UNITY_2021_1_OR_NEWER

using UnityEditor.Overlays;
using UnityEditor.Toolbars;

namespace WarpedImagination.SceneSelectionOverlayTool
{

    /// <summary>
    /// Scene Selection Overlay allows users to select a scene to load
    /// </summary>
    [Overlay(typeof(SceneView), "Scene Selection")]
    public class SceneSelectionOverlay : ToolbarOverlay
    {
        SceneSelectionOverlay() : base(SceneDropdownToggle.ID) { }

        [EditorToolbarElement(ID, typeof(SceneView))]
        class SceneDropdownToggle : EditorToolbarDropdownToggle, IAccessContainerWindow
        {
            public const string ID = "SceneSelectionOverlay/SceneDropdownToggle";

            public EditorWindow containerWindow { get; set; }

            SceneDropdownToggle()
            {
                text = "Scenes";
                tooltip = "Select a scene to load";

                icon = EditorGUIUtility.IconContent("SceneAsset On Icon").image as Texture2D;

                dropdownClicked += ShowSceneMenu;
            }

            void ShowSceneMenu()
            {
                GenericMenu menu = new GenericMenu();

                if (SceneSelectionOverlaySettings.ShowOnlyScenesInBuild)
                {
                    foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                    {
                        string name = Path.GetFileNameWithoutExtension(scene.path);
                        
                        ShowSubmenu(menu, name, scene.path);
                    }
                }
                else
                {
                    string[] sceneGuids = AssetDatabase.FindAssets("t:scene", null);

                    for (int i = 0; i < sceneGuids.Length; i++)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                        string name = Path.GetFileNameWithoutExtension(path);

                        ShowSubmenu(menu, name, path);
                    }
                }

                menu.ShowAsContext();
            }

            void ShowSubmenu(GenericMenu menu, string name, string path)
            {
                if (SceneSelectionOverlaySettings.ShowAdditiveSceneOption)
                {
                    menu.AddItem(new GUIContent(name + "/Single"), false, () => OpenScene( path, OpenSceneMode.Single));

                    if (IsSceneOpen(path))
                    {
                        menu.AddDisabledItem(new GUIContent(name + "/Additive"));
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(name + "/Additive"), false, () => OpenScene( path, OpenSceneMode.Additive));
                    }
                }
                else
                {
                    menu.AddItem(new GUIContent(name), false, () => OpenScene( path, OpenSceneMode.Single));
                }
            }

            /// <summary>
            /// Checks if the scene at the provided path is currently open
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            bool IsSceneOpen(string path)
            {
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    if (string.Compare(EditorSceneManager.GetSceneAt(i).path, path) == 0)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// Open the specified scenes
            /// Checks if any dirty scenes require saving
            /// </summary>
            /// <param name="path"></param>
            /// <param name="openSceneMode"></param>
            void OpenScene(string path, OpenSceneMode openSceneMode)
            {
                if (openSceneMode == OpenSceneMode.Single)
                {
                    // check for scenes being dirty
                    List<Scene> dirtyScenes = null;
                    for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                    {
                        Scene scene = EditorSceneManager.GetSceneAt(i);
                        if (scene.isDirty)
                        {
                            if (dirtyScenes == null)
                                dirtyScenes = new List<Scene>();
                            dirtyScenes.Add(scene);
                        }
                    }

                    // ask user to save any dirty scenes
                    if (dirtyScenes != null)
                    {
                        if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(dirtyScenes.ToArray()))
                        {
                            // returning false from the SaveModifiedScenesIfUserWantsTo means the user cancelled the open scene option
                            return;
                        }
                    }
                }
 
                // open the scene
                EditorSceneManager.OpenScene(path, openSceneMode);
            }
        }
    }
}
#endif