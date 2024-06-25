using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WantedButtons))]
public class WantedButtonsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector elements

        WantedButtons wantedButtons = (WantedButtons)target;

        if (GUILayout.Button("WantedLevel1"))
        {
            wantedButtons.WantedLevel1();
        }

        if (GUILayout.Button("WantedLevel2"))
        {
            wantedButtons.WantedLevel2();
        }

        if (GUILayout.Button("WantedLevel3"))
        {
            wantedButtons.WantedLevel3();
        }

        if (GUILayout.Button("WantedLevel4"))
        {
            wantedButtons.WantedLevel4();
        }

        if (GUILayout.Button("WantedLevel5"))
        {
            wantedButtons.WantedLevel5();
        }
    }
}
