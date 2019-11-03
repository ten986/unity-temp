// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class EnumMask2 {

// }

// public class EnumMask2<T> : EnumMask2
// where T : System.Enum {
//   BitArray Mask;
//   Dictionary<T, int> ToMaskIdx;
//   public EnumMask2 () {
//     ToMaskIdx = new Dictionary<T, int> ();
//     foreach (T t in System.Enum.GetValues(typeof(T))){

//     }
//   }
// }

// [CustomPropertyDrawer (typeof (EnumMask2), true)]
// public class EnumMask2PropertyDrawer : PropertyDrawer {
//   public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
//     object current = GetCurrent (property);
//     if (null != current) {
//       EditorGUI.BeginChangeCheck ();
//       var value = EditorGUI.EnumMaskField (position, label, (System.Enum) current);
//       if (EditorGUI.EndChangeCheck ()) {
//         var size = property.enumNames.Length;
//         var sizemask = 1 << size;
//         property.intValue = (System.Convert.ToInt32 (value) + sizemask) % sizemask;
//       }
//     }
//   }

//   private static object GetCurrent (SerializedProperty property) {
//     object result = property.serializedObject.targetObject;
//     var property_names = property.propertyPath.Replace (".Array.data", ".").Split ('.');
//     foreach (var property_name in property_names) {
//     var parent = result;
//     var indexer_start = property_name.IndexOf ('[');
//     if (-1 == indexer_start) {
//     var binding_flags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
//     result = parent.GetType ().GetField (property_name, binding_flags).GetValue (parent);
//     } else if (parent.GetType ().IsArray) {
//     var indexer_end = property_name.IndexOf (']');
//     var index_string = property_name.Substring (indexer_start + 1, indexer_end - indexer_start - 1);
//     var index = int.Parse (index_string);
//     var array = (System.Array) parent;
//     if (index < array.Length) {
//     result = array.GetValue (index);
//     } else {
//     result = null;
//     break;
//         }
//       } else {
//         throw new System.MissingFieldException ();
//       }
//     }
//     return result;
//   }
// }
