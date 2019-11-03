using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour {

	public static void PlayCorrectSe () {
		Sound.PlaySe ($"maru{Random.Range(1,11)}");
	}

	public static void PlayWrongSe () {
		Sound.PlaySe ($"batsu{Random.Range(1,3)}");
	}

	public static void PlayGameOverSe () {
		Sound.PlaySe ($"gameover{Random.Range(1,6)}");
	}

	// Use this for initialization
	void Start () {

		//決定音
		Sound.LoadSe ("decide", "nyu2");

		//カウントダウn
		Sound.LoadSe ("count","moving-cursor-5");

		//以下今まで使ってたやつ
		Sound.LoadSe ("koke", "chicken-cry1");
		Sound.LoadSe ("gong", "gong-played1");

		Sound.LoadSe ("maru1", "chick-cry1");
		Sound.LoadSe ("maru2", "correct1");
		Sound.LoadSe ("maru3", "correct5");
		Sound.LoadSe ("maru4", "decision11");
		Sound.LoadSe ("maru5", "decision12");
		Sound.LoadSe ("maru6", "kick-high1");
		Sound.LoadSe ("maru7", "ko1");
		Sound.LoadSe ("maru8", "magic-electron2");
		Sound.LoadSe ("maru9", "punch-high2");
		Sound.LoadSe ("maru10", "decision13");

		Sound.LoadSe ("batsu1", "incorrect1");
		Sound.LoadSe ("batsu2", "incorrect2");

		Sound.LoadSe ("gameover1", "costume-drama1");
		Sound.LoadSe ("gameover2", "fate2");
		Sound.LoadSe ("gameover3", "fate1");
		Sound.LoadSe ("gameover4", "broadcasting-end1");
		Sound.LoadSe ("gameover6", "tin1");

		Sound.LoadBgm ("bgm", "oke_song_29_ambasa");
	}

	// Update is called once per frame
	void Update () {

	}
}
