using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PlaylistEntry))]
public class PlaylistEntryPropertyDrawer : PropertyDrawer
{
    private const float lineHeight = 20f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        PlaylistEntryType type = (PlaylistEntryType)property.FindPropertyRelative(nameof(PlaylistEntry.Action)).enumValueIndex;

        float height = 0f;
        switch (type)
        {
            case PlaylistEntryType.WAIT:
                height = lineHeight * 2f;
                break;
            case PlaylistEntryType.PLAY:
                SerializedProperty waveProperty = property.FindPropertyRelative(nameof(PlaylistEntry.Wave));
                height = lineHeight * 2 + EditorGUI.GetPropertyHeight(waveProperty, true);
                break;
            case PlaylistEntryType.PLAY_RANDOM:
                SerializedProperty optionsProperty = property.FindPropertyRelative(nameof(PlaylistEntry.Options));
                height = lineHeight * 2 + EditorGUI.GetPropertyHeight(optionsProperty, true);
                break;
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty typeProperty = property.FindPropertyRelative(nameof(PlaylistEntry.Action));
        EditorGUI.PropertyField(GetLineRect(0, position), typeProperty, new GUIContent($"Action {property.displayName.Split(' ')[1]}"));
        PlaylistEntryType entryType = (PlaylistEntryType)typeProperty.enumValueIndex;

        EditorGUI.indentLevel++;

        switch (entryType)
        {
            case PlaylistEntryType.WAIT:
                EditorGUI.PropertyField(GetLineRect(1, position), property.FindPropertyRelative(nameof(PlaylistEntry.Delay)), new GUIContent("Time"));
                break;
            case PlaylistEntryType.PLAY:
                EditorGUI.PropertyField(GetLineRect(1, position), property.FindPropertyRelative(nameof(PlaylistEntry.WaitForCompletion)), new GUIContent("Wait for completion"));
                EditorGUI.PropertyField(GetLineRect(2, position), property.FindPropertyRelative(nameof(PlaylistEntry.Wave)), new GUIContent("Wave"));
                break;
            case PlaylistEntryType.PLAY_RANDOM:
                EditorGUI.PropertyField(GetLineRect(1, position), property.FindPropertyRelative(nameof(PlaylistEntry.WaitForCompletion)), new GUIContent("Wait for completion"));
                EditorGUI.PropertyField(GetLineRect(2, position), property.FindPropertyRelative(nameof(PlaylistEntry.Options)), new GUIContent("Options"));
                break;
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    private Rect GetLineRect(int line, Rect baseRect)
    {
        return new Rect(baseRect.position.x, baseRect.position.y + lineHeight * line, baseRect.width, lineHeight);
    }
}
