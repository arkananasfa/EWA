using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsShop : MonoBehaviour {

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private Vector2 _hidePosition;

    [SerializeField]
    private float _animationTime;

    [SerializeField]
    private List<BuyButton> _buyButtons;

    private Vector2 _showPosition;
    private Fraction _fraction;
    private FractionType _fractionType;

    private void Start() {
        _showPosition = transform.position;
        Game.Loop.OnMoveStarted += MoveStarted;
        _fractionType = Game.Player1.AllowedFractions[0];
        _fraction = Game.FractionsArchive.GetElementByFractionType(_fractionType).fraction;
        SetButtons();
    }

    public void SetFraction(FractionType fractionType) {
        _content.SetActive(true);
        if (fractionType == _fractionType)
            return;

        PlayChangeFractionAnimation(fractionType);
    }

    public void Hide() {
        _content.SetActive(false);
    }

    private void MoveStarted() {
        if (!Game.CurrentPlayer.AllowedFractions.Contains(_fractionType))
            Hide();
    }

    private void PlayChangeFractionAnimation(FractionType fractionType) {
        _fractionType = fractionType;
        _fraction = Game.FractionsArchive.GetElementByFractionType(fractionType).fraction;
        StartCoroutine(ChangeFractionRoutine());
    }

    private IEnumerator ChangeFractionRoutine() {
        float time = 0f;
        bool changed = false;
        while (time < _animationTime) {
            yield return null;
            time += Time.deltaTime;
            transform.position = HalfLerpPosition(time);
            if (!changed && time > _animationTime/2f) {
                changed = true;
                SetButtons();
            }
        }
        transform.position = _showPosition;
        yield return null;
    }

    private void SetButtons() {
        for (int i = 0; i < _buyButtons.Count; i++) {
            if (i < _fraction.Units.Count) {
                _buyButtons[i].gameObject.SetActive(true);
                _buyButtons[i].Init(_fraction.Units[i]);
            } else {
                _buyButtons[i].Hide();
            }
        }
    }

    private Vector3 HalfLerpPosition (float time) {
        return Vector3.Lerp(_hidePosition, _showPosition, Mathf.Abs(time/(_animationTime/2f)-1));
    }

}