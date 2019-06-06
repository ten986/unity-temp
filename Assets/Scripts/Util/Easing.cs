using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Functions taken from Tween.js - Licensed under the MIT license
 * at https://github.com/sole/tween.js
 */
public class Easing {
	public enum Type {
		/// <summary>
		/// 線形補間
		/// </summary>
		Linear,
		/// <summary>
		///二次
		/// </summary>
		Quad,
		/// <summary>
		/// 三次
		/// </summary>
		Cubic,
		/// <summary>
		/// 四次
		/// </summary>
		Quart,
		/// <summary>
		/// 五次
		/// </summary>
		Quint,
		/// <summary>
		/// サイン
		/// </summary>
		Sin,
		/// <summary>
		/// 指数
		/// </summary>
		Expo,
		/// <summary>
		/// 引っ張ったバネを離したときのような動き
		/// </summary>
		Elastic,
		/// <summary>
		/// 行き過ぎて、少し戻るような動き
		/// </summary>
		Back,
		/// <summary>
		/// 跳ねる
		/// </summary>
		Bounce,
		/// <summary>
		/// 円
		/// </summary>
		Circ,
	}

	public enum InOut {
		In,
		Out,
		InOut,
	}

	/// <summary>
	/// EasingFunctionを呼び出す
	/// </summary>
	/// <param name="easingType">種類</param>
	/// <param name="inoutType">InかOutかInOutか</param>
	/// <param name="k">k</param>
	/// <param name="firstValue">k=firstValueで0を返す デフォルト値0</param>
	/// <param name="lastValue">k=lastValueで1を返す デフォルト値1</param>
	/// <returns></returns>
	public static float Ease (Type easingType, InOut inoutType, float k, float firstValue = 0, float lastValue = 1) {
		if (firstValue > lastValue) {
			float tmp = firstValue;
			firstValue = lastValue;
			lastValue = tmp;

			float kk = Mathf.InverseLerp (firstValue, lastValue, k);
			k = Mathf.Lerp (firstValue, lastValue, 1 - kk);
		}

		if (k < firstValue) {
			return 0;
		}
		if (k > lastValue) {
			return 1;
		}
		float newK = Mathf.InverseLerp (firstValue, lastValue, k);
		switch (easingType) {
			case Type.Linear:
				return Linear (newK);
			case Type.Quad:
				switch (inoutType) {
					case InOut.In:
						return Quadratic.In (newK);
					case InOut.Out:
						return Quadratic.Out (newK);
					case InOut.InOut:
						return Quadratic.InOut (newK);
				}
				break;
			case Type.Cubic:
				switch (inoutType) {
					case InOut.In:
						return Cubic.In (newK);
					case InOut.Out:
						return Cubic.Out (newK);
					case InOut.InOut:
						return Cubic.InOut (newK);
				}
				break;
			case Type.Quart:
				switch (inoutType) {
					case InOut.In:
						return Quartic.In (newK);
					case InOut.Out:
						return Quartic.Out (newK);
					case InOut.InOut:
						return Quartic.InOut (newK);
				}
				break;
			case Type.Quint:
				switch (inoutType) {
					case InOut.In:
						return Quintic.In (newK);
					case InOut.Out:
						return Quintic.Out (newK);
					case InOut.InOut:
						return Quintic.InOut (newK);
				}
				break;
			case Type.Sin:
				switch (inoutType) {
					case InOut.In:
						return Sinusoidal.In (newK);
					case InOut.Out:
						return Sinusoidal.Out (newK);
					case InOut.InOut:
						return Sinusoidal.InOut (newK);
				}
				break;
			case Type.Expo:
				switch (inoutType) {
					case InOut.In:
						return Exponential.In (newK);
					case InOut.Out:
						return Exponential.Out (newK);
					case InOut.InOut:
						return Exponential.InOut (newK);
				}
				break;
			case Type.Elastic:
				switch (inoutType) {
					case InOut.In:
						return Elastic.In (newK);
					case InOut.Out:
						return Elastic.Out (newK);
					case InOut.InOut:
						return Elastic.InOut (newK);
				}
				break;
			case Type.Back:
				switch (inoutType) {
					case InOut.In:
						return Back.In (newK);
					case InOut.Out:
						return Back.Out (newK);
					case InOut.InOut:
						return Back.InOut (newK);
				}
				break;
			case Type.Bounce:
				switch (inoutType) {
					case InOut.In:
						return Bounce.In (newK);
					case InOut.Out:
						return Bounce.Out (newK);
					case InOut.InOut:
						return Bounce.InOut (newK);
				}
				break;
			case Type.Circ:
				switch (inoutType) {
					case InOut.In:
						return Circular.In (newK);
					case InOut.Out:
						return Circular.Out (newK);
					case InOut.InOut:
						return Circular.InOut (newK);
				}
				break;
		}
		throw new InvalidOperationException ();
	}

	public static float Linear (float k) {
		return k;
	}

	public class Quadratic {
		public static float In (float k) {
			return k * k;
		}

		public static float Out (float k) {
			return k * (2f - k);
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f * k * k;
			return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
		}
	};

	public class Cubic {
		public static float In (float k) {
			return k * k * k;
		}

		public static float Out (float k) {
			return 1f + ((k -= 1f) * k * k);
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f * k * k * k;
			return 0.5f * ((k -= 2f) * k * k + 2f);
		}
	};

	public class Quartic {
		public static float In (float k) {
			return k * k * k * k;
		}

		public static float Out (float k) {
			return 1f - ((k -= 1f) * k * k * k);
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f * k * k * k * k;
			return -0.5f * ((k -= 2f) * k * k * k - 2f);
		}
	};

	public class Quintic {
		public static float In (float k) {
			return k * k * k * k * k;
		}

		public static float Out (float k) {
			return 1f + ((k -= 1f) * k * k * k * k);
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f * k * k * k * k * k;
			return 0.5f * ((k -= 2f) * k * k * k * k + 2f);
		}
	};

	public class Sinusoidal {
		public static float In (float k) {
			return 1f - Mathf.Cos (k * Mathf.PI / 2f);
		}

		public static float Out (float k) {
			return Mathf.Sin (k * Mathf.PI / 2f);
		}

		public static float InOut (float k) {
			return 0.5f * (1f - Mathf.Cos (Mathf.PI * k));
		}
	};

	public class Exponential {
		public static float In (float k) {
			return k == 0f? 0f : Mathf.Pow (1024f, k - 1f);
		}

		public static float Out (float k) {
			return k == 1f? 1f : 1f - Mathf.Pow (2f, -10f * k);
		}

		public static float InOut (float k) {
			if (k == 0f) return 0f;
			if (k == 1f) return 1f;
			if ((k *= 2f) < 1f) return 0.5f * Mathf.Pow (1024f, k - 1f);
			return 0.5f * (-Mathf.Pow (2f, -10f * (k - 1f)) + 2f);
		}
	};

	public class Circular {
		public static float In (float k) {
			return 1f - Mathf.Sqrt (1f - k * k);
		}

		public static float Out (float k) {
			return Mathf.Sqrt (1f - ((k -= 1f) * k));
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return -0.5f * (Mathf.Sqrt (1f - k * k) - 1);
			return 0.5f * (Mathf.Sqrt (1f - (k -= 2f) * k) + 1f);
		}
	};

	public class Elastic {
		public static float In (float k) {
			if (k == 0) return 0;
			if (k == 1) return 1;
			return -Mathf.Pow (2f, 10f * (k -= 1f)) * Mathf.Sin ((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
		}

		public static float Out (float k) {
			if (k == 0) return 0;
			if (k == 1) return 1;
			return Mathf.Pow (2f, -10f * k) * Mathf.Sin ((k - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return -0.5f * Mathf.Pow (2f, 10f * (k -= 1f)) * Mathf.Sin ((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
			return Mathf.Pow (2f, -10f * (k -= 1f)) * Mathf.Sin ((k - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
		}
	};

	public class Back {
		static float s = 1.70158f;
		static float s2 = 2.5949095f;

		public static float In (float k) {
			return k * k * ((s + 1f) * k - s);
		}

		public static float Out (float k) {
			return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
		}

		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f * (k * k * ((s2 + 1f) * k - s2));
			return 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
		}
	};

	public class Bounce {
		public static float In (float k) {
			return 1f - Out (1f - k);
		}

		public static float Out (float k) {
			if (k < (1f / 2.75f)) {
				return 7.5625f * k * k;
			} else if (k < (2f / 2.75f)) {
				return 7.5625f * (k -= (1.5f / 2.75f)) * k + 0.75f;
			} else if (k < (2.5f / 2.75f)) {
				return 7.5625f * (k -= (2.25f / 2.75f)) * k + 0.9375f;
			} else {
				return 7.5625f * (k -= (2.625f / 2.75f)) * k + 0.984375f;
			}
		}

		public static float InOut (float k) {
			if (k < 0.5f) return In (k * 2f) * 0.5f;
			return Out (k * 2f - 1f) * 0.5f + 0.5f;
		}
	};
}
