using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour {

    private TextMeshProUGUI _text;
    private Action _callback;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void StartTimer(MonoBehaviour context) {
        StartCoroutine(StartTimerRoutine(3));
    }

    public void SetCallback(Action callback) {
        _callback = callback;
    }

    private IEnumerator StartTimerRoutine(int seconds) {
        _text.text = $"Game starts in {seconds} seconds";
        float time = seconds;
        while (time > 0) {
            yield return null;
            time -= Time.deltaTime;
            _text.text = $"Game starts in {Mathf.RoundToInt((int)time)} seconds";
        }
        _callback?.Invoke();
    }

}