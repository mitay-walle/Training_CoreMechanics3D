using System;
using NaughtyAttributes;
using UnityEngine;

namespace GameJam.Plugins.Timing
{
	[Serializable]
	public struct Timer : IComparable<float>
	{
		public float duration;
		private float endTime;

		[ShowNativeProperty] private float ElapsedTime => GetTimeElapsed();

		public Timer(float duration)
		{
			this.duration = duration;
			endTime = 0;
		}

		public void SetDuration(float newDuration)
		{
			duration = newDuration;
			Restart(newDuration);
		}

		public bool CheckAndRestart()
		{
			bool isComplete = IsReady();
			if (isComplete)
				Restart();
			return isComplete;
		}

		public void Cancel()
		{
			endTime = float.MaxValue;
		}

		public bool CheckAndCancel()
		{
			bool isComplete = IsReady();
			if (isComplete)
				Cancel();
			return isComplete;
		}

		public bool IsReady() => Time.time >= endTime;
		public void Restart() => endTime = Time.time + duration;

		public void Restart(float time)
		{
			duration = time;
			endTime = Time.time + time;
		}

		public void Reset() => endTime = Time.time;
		public void Reset(float time) => endTime = Time.time + time;
		public float GetTimeLeft() => Mathf.Max(0, endTime - Time.time);
		public float GetTimeElapsed() => Mathf.Max(0, duration - GetTimeLeft());
		public float GetStartTime() => endTime - duration;
		public void Complete() => endTime = float.MinValue;

		public static explicit operator float(Timer source) => source.duration;

		public int CompareTo(float other)
		{
			return duration.CompareTo(other);
		}
	}
}