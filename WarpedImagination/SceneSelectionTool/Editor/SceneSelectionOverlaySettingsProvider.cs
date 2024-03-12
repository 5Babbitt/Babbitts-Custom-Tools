//
// Copyright (c) 2023 Warped Imagination. All rights reserved. 
//

using UnityEditor;
using UnityEngine;

namespace WarpedImagination.SceneSelectionOverlayTool
{
    /// <summary>
    /// Displays entries for the Scene Selection Overlay Tool settings under the preferences window
    /// </summary>
    public class SceneSelectionOverlaySettingsProvider : SettingsProvider
    {
        GUIContent _additiveOptionContent = null;
        GUIContent _buildOnlyScenesContent = null;

        public SceneSelectionOverlaySettingsProvider(string path, SettingsScope scope)
            : base(path, scope)
        { }

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);

            GUILayout.Space(20f);

            if(_additiveOptionContent == null)
                _additiveOptionContent = new GUIContent("Additive Scene Option", "Turns on the submenu for Additive scene loading");

            bool showAdditiveSceneOption = EditorGUILayout.Toggle(_additiveOptionContent, SceneSelectionOverlaySettings.ShowAdditiveSceneOption);
            if(showAdditiveSceneOption != SceneSelectionOverlaySettings.ShowAdditiveSceneOption)
            {
                SceneSelectionOverlaySettings.ShowAdditiveSceneOption = showAdditiveSceneOption;
            }

            if (_buildOnlyScenesContent == null)
                _buildOnlyScenesContent = new GUIContent("Only Scenes In The Build", "Only shows scenes listed in the build");

            bool enabled = EditorGUILayout.Toggle(_buildOnlyScenesContent, SceneSelectionOverlaySettings.ShowOnlyScenesInBuild);
            if (enabled != SceneSelectionOverlaySettings.ShowOnlyScenesInBuild)
            {
                SceneSelectionOverlaySettings.ShowOnlyScenesInBuild = enabled;
            }
        }


        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            // Note: change the first argument path to move these settings elsewhere under Preferences window
            // Note: change second argument if you prefer the settings to be under Player Settings
            return new SceneSelectionOverlaySettingsProvider("Preferences/Tools/Scene Selection Tool", SettingsScope.User);
        }

    }
}