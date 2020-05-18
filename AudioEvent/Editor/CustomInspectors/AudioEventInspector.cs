using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioEvent), true)]
public class AudioEventInspector : Editor
{
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource =
            EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(audioSource.gameObject);
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioEvent) target).Play(audioSource);
        }

        EditorGUI.EndDisabledGroup();
    }
}