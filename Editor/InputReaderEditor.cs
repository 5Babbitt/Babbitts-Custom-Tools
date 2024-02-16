using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputReader))]
public class InputReaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InputReader reader = (InputReader)target;

        GUILayout.Space(20);
        GUILayout.Label($"Current Input Mapping:\n{reader.currentInputMap}", GetLableStyle());

        GUILayout.Space(20);
        if (GUILayout.Button("Set Gameplay Input"))
        {
            reader.SetGameplay();
        }

        if (GUILayout.Button("Set UI Input"))
        {
            reader.SetUI();
        }
    }

    GUIStyle GetLableStyle()
    {
        GUIStyle lableStyle = new GUIStyle(EditorStyles.largeLabel);

        lableStyle.fontSize = 24;
        lableStyle.alignment = TextAnchor.MiddleCenter;

        return lableStyle;
    }
}
