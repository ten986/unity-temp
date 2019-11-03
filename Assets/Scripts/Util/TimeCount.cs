using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount :MonoBehaviour {

	float time;//時間を記録する小数も入る変数.
	float oldtime;
	string text;
	bool _isplay;

	public void TimeStart(){
		_isplay = true;
	}

	public void TimeStop(){
		_isplay = false;
	}

	public void TimeReset(){
		time = 0;
		oldtime = 0;
	}

	public void TimeReStart(){
		time = 0;
		oldtime = 0;
		_isplay = true;
	}

	public float GetTime(){
		return time;
	}

	public string GetTimeString(){
		return Float2TimeString (time);	
	}

	void Start () {
		time = 0;
		oldtime = 0;
		text = "";
		_isplay = false;
	}

	public static string Float2TimeString(float time){
		int minute = (int)time/60;//分.timeを60で割った値.
		int second = (int)time%60;//秒.timeを60で割った余り.
		int mili= (int)((time*100)%100);
		string minText, secText,miliText;//テキスト形式の分・秒を用意.
		if (minute < 10)
			minText = "0" + minute.ToString();//ToStringでint→stringに変換.
		else
			minText = minute.ToString();
		if (second < 10)
			secText = "0" + second.ToString();//上に同じく.
		else
			secText = second.ToString();
		if (mili < 10)
			miliText = "0" + mili.ToString();//上に同じく.
		else
			miliText = mili.ToString();

		return "" + minText + "′" + secText+"″"+ miliText;
	}

	void Update () {
		if (_isplay) {
			time += Time.deltaTime;//毎フレームの時間を加算.
		}

		text = Float2TimeString (time);

		oldtime = time;
	}
}