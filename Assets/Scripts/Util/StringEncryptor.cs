//  StringEncryptor.cs
//  http://kan-kikuchi.hatenablog.com/entry/Json_SaveData
//
//  Created by kan.kikuchi on 2016.11.21.

using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 文字列の暗号化、復号化を行うクラス
/// </summary>
public static class StringEncryptor {

    //=================================================================================
    //暗号化
    //=================================================================================

    /// <summary>
    /// 文字列を暗号化する
    /// </summary>
    public static string Encrypt (string sourceString) {

        //RijndaelManagedオブジェクトを作成
        RijndaelManaged rijndael =
            new RijndaelManaged ();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword (
            rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に変換する
        byte[] strBytes = System.Text.Encoding.UTF8.GetBytes (sourceString);

        //対称暗号化オブジェクトの作成
        ICryptoTransform encryptor =
            rijndael.CreateEncryptor ();

        //バイト型配列を暗号化する
        byte[] encBytes = encryptor.TransformFinalBlock (strBytes, 0, strBytes.Length);

        //閉じる
        encryptor.Dispose ();

        //バイト型配列を文字列に変換して返す
        return System.Convert.ToBase64String (encBytes);
    }

    //=================================================================================
    //復号化
    //=================================================================================

    /// <summary>
    /// 暗号化された文字列を復号化する
    /// </summary>
    public static string Decrypt (string sourceString) {

        //RijndaelManagedオブジェクトを作成
        RijndaelManaged rijndael =
            new RijndaelManaged ();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword (
            rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に戻す
        byte[] strBytes = System.Convert.FromBase64String (sourceString);

        //対称暗号化オブジェクトの作成
        ICryptoTransform decryptor =
            rijndael.CreateDecryptor ();

        //バイト型配列を復号化する
        //復号化に失敗すると例外CryptographicExceptionが発生
        byte[] decBytes = decryptor.TransformFinalBlock (strBytes, 0, strBytes.Length);

        //閉じる
        decryptor.Dispose ();

        //バイト型配列を文字列に戻して返す
        return System.Text.Encoding.UTF8.GetString (decBytes);
    }

    //=================================================================================
    //共通
    //=================================================================================

    //パスワードから共有キーと初期化ベクタを生成する
    private static void GenerateKeyFromPassword (int keySize, out byte[] key, int blockSize, out byte[] iv) {
        //パスワードから共有キーと初期化ベクタを作成する
        //saltを決める
        byte[] salt = System.Text.Encoding.UTF8.GetBytes ("0XSgeXPgyAlUVuAQhn9LzryFQRQsqJmzD1qNDYPBjzkh2RwRGVDxaHNzTYrqX++weEvgylpCVkDDyHpf11NU+1K3rIGK3grEHt4LKmwniDYNVf1SkeUut0AGYz7s75FzW9Wgpc1fjQyQpFFP2RRekXk2l7OkoxbFY4ppAcFCMIZ68rCwEeefz0Gh0D9rW/ntx93ZmTQbIDqAnfDf9/LFmwrw5plsXIHDvoqD8552lBho29Cyz3K5kJnHrHxDz8SJ");

        //Rfc2898DeriveBytesオブジェクトを作成する
        string pass = "7Mo3tKsQC39S3vLRIyVDaWcwSQrldnkvTt1xrCttFaAO545ZPZkwAks+GMo3kn1/EIFlPd1pLGj/aC4f7T/vIAWuVgptzKsswFYM4VdeFwA4pFcxhOzIk8f4BRakyjyuBBlX5jrcFKv6FHSEF3JcUJcOdiF2dCwcvoY6ZjtuiPOhEahHK9Pr/pn0h6fXuUXndGjcA1mPmBxIXiRlVz74b2jLMQiYvoRB5NZPmvTe+G2R8TZOhsb24lleM40OqsaW";

        Rfc2898DeriveBytes deriveBytes =
            new Rfc2898DeriveBytes (pass, salt);

        //反復処理回数を指定する デフォルトで1000回
        deriveBytes.IterationCount = 1000;

        //共有キーと初期化ベクタを生成する
        key = deriveBytes.GetBytes (keySize / 8);
        iv = deriveBytes.GetBytes (blockSize / 8);
    }

}
