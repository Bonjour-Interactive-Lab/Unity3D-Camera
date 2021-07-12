
using UnityEditor;
using UnityEngine;
using Bonjour.Webcam;

namespace Bonjour.Webcam
{
    [CustomEditor(typeof(Bonjour.Webcam.WebcamInput))]
    sealed class WebcamInputEditor : Editor
    {
        static readonly GUIContent SelectLabel = new GUIContent("Select");

        SerializedProperty _deviceName;
        SerializedProperty _resolution;
        SerializedProperty _frameRate;
        SerializedProperty _targetBuffer;
        SerializedProperty _clampFPSToCameraFPS;
        SerializedProperty _vertMirror;
        SerializedProperty _horMirror;

        void OnEnable()
        {
            _deviceName             = serializedObject.FindProperty("deviceName");
            _resolution             = serializedObject.FindProperty("resolution");
            _frameRate              = serializedObject.FindProperty("frameRate");
            _targetBuffer           = serializedObject.FindProperty("targetBuffer");
            _clampFPSToCameraFPS    = serializedObject.FindProperty("clampFPSToCameraFPS");
            _vertMirror             = serializedObject.FindProperty("vertMirror");
            _horMirror              = serializedObject.FindProperty("horMirror");
        }

        void ShowDeviceSelector(Rect rect)
        {
            var menu = new GenericMenu();

            foreach (var device in WebCamTexture.devices)
                menu.AddItem(new GUIContent(device.name), false,
                            () => { serializedObject.Update();
                                    _deviceName.stringValue = device.name;
                                    serializedObject.ApplyModifiedProperties(); });

            menu.DropDown(rect);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(Application.isPlaying);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(_deviceName);

            var rect = EditorGUILayout.GetControlRect(false, GUILayout.Width(60));
            if (EditorGUI.DropdownButton(rect, SelectLabel, FocusType.Keyboard))
                ShowDeviceSelector(rect);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(_resolution);
            EditorGUILayout.PropertyField(_frameRate);
            EditorGUILayout.PropertyField(_targetBuffer);
            EditorGUILayout.PropertyField(_clampFPSToCameraFPS);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(_vertMirror);
            EditorGUILayout.PropertyField(_horMirror);


            serializedObject.ApplyModifiedProperties();
        }
    }
}
