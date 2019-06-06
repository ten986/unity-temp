//  SaveData.cs
//  http://kan-kikuchi.hatenablog.com/entry/Json_SaveData
//
//  Created by kan.kikuchi on 2016.11.21.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// クラスを丸ごとJsonで保存するデータクラス
/// </summary>
[Serializable]
public class SaveData : ISerializationCallbackReceiver {

  //シングルトンを実装するための実体、初アクセス時にLoadする。
  private static SaveData _instance = null;
  public static SaveData Instance {
    get {
      if (_instance == null) {
        Load ();
      }
      return _instance;
    }
  }

  //SaveDataをJsonに変換したテキスト(リロード時に何度も読み込まなくていいように保持)
  [SerializeField]
  private static string _jsonText = "";

  private static string prefsStr = "Save";

  //=================================================================================
  //保存されるデータ(public or SerializeFieldを付ける)
  //=================================================================================

  public string PlayerName = "";
  public bool PlayerConsent = false;

  [SerializeField]
  private string _hiscore = "";
  public Dictionary<Level, int> HiScore = new Dictionary<Level, int>();
  //=================================================================================
  //シリアライズ,デシリアライズ時のコールバック
  //=================================================================================

  /// <summary>
  /// SaveData→Jsonに変換される前に実行される。
  /// </summary>
  public void OnBeforeSerialize () {

  }

  /// <summary>
  /// Json→SaveDataに変換された後に実行される。
  /// </summary>
  public void OnAfterDeserialize () {

  }

  //引数のオブジェクトをシリアライズして返す
  private static string Serialize<T> (T obj) {
    BinaryFormatter binaryFormatter = new BinaryFormatter ();
    MemoryStream memoryStream = new MemoryStream ();
    binaryFormatter.Serialize (memoryStream, obj);
    return Convert.ToBase64String (memoryStream.GetBuffer ());
  }

  //引数のテキストを指定されたクラスにデシリアライズして返す
  private static T Deserialize<T> (string str) {
    BinaryFormatter binaryFormatter = new BinaryFormatter ();
    MemoryStream memoryStream = new MemoryStream (Convert.FromBase64String (str));
    return (T) binaryFormatter.Deserialize (memoryStream);
  }

  //=================================================================================
  //取得
  //=================================================================================

  /// <summary>
  /// データを再読み込みする。
  /// </summary>
  public void Reload () {
    JsonUtility.FromJsonOverwrite (GetJson (), this);
  }

  //データを読み込む。
  private static void Load () {
    _instance = JsonUtility.FromJson<SaveData> (GetJson ());
  }

  //保存しているJsonを取得する
  private static string GetJson () {
    //既にJsonを取得している場合はそれを返す。
    if (!string.IsNullOrEmpty (_jsonText)) {
      return _jsonText;
    }

    //Jsonが存在するか調べてから取得し変換する。存在しなければ新たなクラスを作成し、それをJsonに変換する。
    if (PlayerPrefs.HasKey (prefsStr)) {
      _jsonText = StringEncryptor.Decrypt (PlayerPrefs.GetString (prefsStr));
    } else {
      _jsonText = JsonUtility.ToJson (new SaveData ());
    }

    return _jsonText;
  }

  //=================================================================================
  //保存
  //=================================================================================

  /// <summary>
  /// データをJsonにして保存する。
  /// </summary>
  public void Save () {
    _jsonText = JsonUtility.ToJson (this);
    PlayerPrefs.SetString (prefsStr, StringEncryptor.Encrypt (_jsonText));
    PlayerPrefs.Save ();
  }

  //=================================================================================
  //削除
  //=================================================================================

  /// <summary>
  /// データを全て削除し、初期化する。
  /// </summary>
  public void Delete () {
    _jsonText = JsonUtility.ToJson (new SaveData ());
    Reload ();
  }
}
