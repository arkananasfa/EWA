using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour {

    public event Action OnTimeEnded;

    private TextMeshProUGUI _text;

    private float _currentTime;
    private bool _isPlaying;

    public void Set(float seconds) {
        if (_isPlaying) {
            _currentTime = seconds;
        } else {
            _isPlaying = true;
            StartCoroutine(TimeGoingRoutine());
        }
    }

    private IEnumerator TimeGoingRoutine() {
        while (_currentTime > 0) {
            _currentTime -= Time.deltaTime;
            _text.text = FloatToTimeString(_currentTime);
            yield return null;
        }
        OnTimeEnded?.Invoke();
    }

    private string FloatToTimeString(float seconds) {
        string result = "";
        int timeSeconds = (int)seconds;
        if (timeSeconds < 10) result += "0" + timeSeconds.ToString();
        else result += timeSeconds.ToString();
        int timeMiliseconds = (int)((seconds - timeSeconds) * 100);
        if (timeMiliseconds < 10) result += "0" + timeMiliseconds.ToString();
        else result += timeMiliseconds.ToString();
        return result;
    }

}