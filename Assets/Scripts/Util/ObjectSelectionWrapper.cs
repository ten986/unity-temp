using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class ObjectSelectorWrapper {
    #if UNITY_EDITOR
    private static System.Type TSelector;
    private static bool oldState = false;
    static ObjectSelectorWrapper () {
        TSelector = System.Type.GetType ("UnityEditor.ObjectSelector,UnityEditor");
    }

    private static EditorWindow Get () {
        PropertyInfo P = TSelector.GetProperty ("get", BindingFlags.Public | BindingFlags.Static);
        return P.GetValue (null, null) as EditorWindow;
    }
    public static void ShowSelector (UnityEngine.Object obj, System.Type aRequiredType, bool allowSceneObjects, List<int> allowedInstanceIDs) {
        MethodInfo ShowMethod = TSelector.GetMethod
        ("Show", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, CallingConventions.Any,
        new [] {typeof(UnityEngine.Object),typeof(System.Type),typeof(SerializedProperty),typeof(bool),typeof(List<int>) }
        ,null);
        ShowMethod.Invoke (Get (), new object[] { obj, aRequiredType, null, allowSceneObjects, allowedInstanceIDs });
    }
    public static void ShowSelector (System.Type aRequiredType) {
        MethodInfo ShowMethod = TSelector.GetMethod ("Show", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        ShowMethod.Invoke (Get (), new object[] { null, aRequiredType, null, true });
    }
    public static T GetSelectedObject<T> () where T : UnityEngine.Object {
        MethodInfo GetCurrentObjectMethod = TSelector.GetMethod ("GetCurrentObject", BindingFlags.Static | BindingFlags.Public);
        return GetCurrentObjectMethod.Invoke (null, null) as T;
    }
    public static bool isVisible {
        get {
            PropertyInfo P = TSelector.GetProperty ("isVisible", BindingFlags.Public | BindingFlags.Static);
            return (bool) P.GetValue (null, null);
        }
    }
    public static bool HasJustBeenClosed () {
        bool visible = isVisible;
        if (visible != oldState && visible == false) {
            oldState = false;
            return true;
        }
        oldState = visible;
        return false;
    }
#endif
}
