using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class LifeBar : MonoBehaviour {
		public float maxPoints;

		[SerializeField] private float           lerpSpeed = 2;
		[SerializeField] private Slider          slider;
		[SerializeField] private TextMeshProUGUI text;
		private                  bool            _lerpFlag;

		public LifeBar() {
			Instance = this;
		}

		public static LifeBar Instance { get; private set; }

		public float Points { get; private set; }

		private void Start() {
			//_points = maxPoints;

			slider.value = Points / maxPoints;
			text.text    = $"{Points} / {maxPoints}";
		}

		public void AddPoints(float points) {
			var start = Points;
			Points = Math.Min(Points + points, maxPoints);

			StartCoroutine(Lerp(start));
		}

		public void RemovePoints(float points) {
			var start = Points;
			Points = Math.Max(Points - points, 0f);

			StartCoroutine(Lerp(start));

			if (Points <= 0f) {
				OnDie();
			}
		}

		private void OnDie() {}

		private IEnumerator Lerp(float start) {
			if (_lerpFlag) {
				yield return null;
			}

			_lerpFlag = true;

			var timeScale = 0f;
			while (timeScale < 1) {
				timeScale += Time.deltaTime * lerpSpeed;
				var lerp = Mathf.Lerp(start, Points, timeScale);
				slider.value = lerp / maxPoints;
				text.text    = $"{(int) lerp} / {maxPoints}";

				yield return null;
			}

			_lerpFlag = false;
		}
	}
}