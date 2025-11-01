#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Building b = (Building)target;
        if (string.IsNullOrEmpty(b.UniqueID))
        {
            if (GUILayout.Button("Generate Unique ID"))
            {
                b.GenerateID();
                EditorUtility.SetDirty(b);
            }
        }
    }
}
#endif