//
// Copyright (c) 2022 Warped Imagination. All rights reserved. 
//

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace WarpedImagination.SceneSelectionOverlayTool
{
    /// <summary>
    /// Holds the settings for the Scene Selection Overlay Tool
    /// </summary>
    public static class SceneSelectionOverlaySettings
    {
        // Note: change these if you want to save the preferences under different names
        const string ADDITIVE_LOADING_PREF = "ShowAdditiveSceneOption";
        const string BUILD_ONLY_PREF = "ShowOnlyBuildScenes";

        static bool? _showAdditiveSceneLoading = null;
        static bool? _buildOnlyScenes = null;

        /// <summary>
        /// Whether or not the submenu for Additive loading is enabled
        /// </summary>
        public static bool ShowAdditiveSceneOption
        {
            get
            {
                if (!_showAdditiveSceneLoading.HasValue)
                    _showAdditiveSceneLoading = EditorPrefs.GetBool(ADDITIVE_LOADING_PREF, true);
                return _showAdditiveSceneLoading.Value;
            }
            set
            {
                _showAdditiveSceneLoading = value;
                EditorPrefs.SetBool(ADDITIVE_LOADING_PREF, _showAdditiveSceneLoading.Value);
            }
        }

        /// <summary>
        /// Whether or not to only show build scenes
        /// </summary>
        public static bool ShowOnlyScenesInBuild
        {
            get
            {
                if (!_buildOnlyScenes.HasValue)
                    _buildOnlyScenes = EditorPrefs.GetBool(BUILD_ONLY_PREF, true);
                return _buildOnlyScenes.Value;
            }
            set
            {
                _buildOnlyScenes = value;
                EditorPrefs.SetBool(BUILD_ONLY_PREF, _buildOnlyScenes.Value);
            }
        }
    }


}