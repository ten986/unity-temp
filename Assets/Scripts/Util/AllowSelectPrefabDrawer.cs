using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer (typeof (AllowSelectPrefabAttribute))]
public class AllowSelectPrefabDrawer : PropertyDrawer {
    static Dictionary<string, int> currentPickerWindow = new Dictionary<string, int> ();
    static Dictionary<string, string> tempLabel = new Dictionary<string, string> ();

    static int counter = 0;

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        var allowSelectPrefabAttribute = (AllowSelectPrefabAttribute) attribute;
        label = EditorGUI.BeginProperty (position, label, property);

        string commandName = Event.current.commandName;
        if (currentPickerWindow.TryGetValue (property.propertyPath, out int id))
            if (commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID () == id) {
                property.objectReferenceValue = (EditorGUIUtility.GetObjectPickerObject () as GameObject);
                //ObjectPickerを開いている間はEditorWindowの再描画が行われないのでRepaintを呼びだす
                //Repaint ();
            } else if (commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID () == id) {
            property.objectReferenceValue = (EditorGUIUtility.GetObjectPickerObject () as GameObject) ?? property.objectReferenceValue;
            var l = Resources.LoadAll ("Prefabs/", GetPropertyType (property))
                .Where (x => x.GetType ().Equals (GetPropertyType (property)))
                .OfType<MonoBehaviour> ()
                .Select (x => x.gameObject)
                .ToList ();
            foreach (var obj in l) {
                AssetDatabase.SetLabels (obj, AssetDatabase.GetLabels (obj).Where (s => !s.Equals (tempLabel[property.propertyPath])).ToArray ());
            }
        }

        float space = 50;
        var propertyPosition = new Rect (position) { width = position.width - space };
        var buttonPosition = new Rect (position) { x = position.width - space + 13, width = space };
        EditorGUI.PropertyField (propertyPosition, property, label);
        if (GUI.Button (buttonPosition, "ほげ")) {
            string templab = "temp_label" + GetTimeStamp ();
            if (!tempLabel.ContainsKey (property.propertyPath)) {
                tempLabel.Add (property.propertyPath, templab);
            }
            tempLabel[property.propertyPath] = templab;
            var l = Resources.LoadAll ("Prefabs/", GetPropertyType (property))
                .Where (x => x.GetType ().Equals (GetPropertyType (property)))
                .OfType<MonoBehaviour> ()
                .Select (x => x.gameObject)
                .ToList ();
            foreach (var obj in l) {
                AssetDatabase.SetLabels (obj, AssetDatabase.GetLabels (obj).Append (templab).ToArray ());
            }

            //int controlID = EditorGUIUtility.GetControlID (label,FocusType.Passive);
            int controlID = counter++;
            if (!currentPickerWindow.ContainsKey (property.propertyPath)) {
                currentPickerWindow.Add (property.propertyPath, controlID);
            }
            currentPickerWindow[property.propertyPath] = controlID;
            //CameraのコンポーネントをタッチしているGameObjectを選択する
            EditorGUIUtility.ShowObjectPicker<GameObject> (null, false, "l:" + templab, controlID);
        }

        EditorGUI.EndProperty ();
    }

    Type GetPropertyType (SerializedProperty property) {
        var type = property.type;
        var match = Regex.Match (type, @"PPtr<\$(.*?)>");
        return match.Success ? Type.GetType (match.Groups[1].Value) : throw new ArgumentException ();
    }

    string GetTimeStamp () {
        var now = DateTime.UtcNow;
        long unixtime = (long) (now - new DateTime (1970, 1, 1)).TotalSeconds;
        return unixtime.ToString ();
    }
}
#endif
