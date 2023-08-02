using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour {

    public Unit Unit => _context;
    public Sprite Sprite => _image.sprite;

    [SerializeField]
    private ColorSkillStatusIndicator _moveIndicator;

    [SerializeField]
    private ColorSkillStatusIndicator _attackIndicator;

    [SerializeField]
    private float _spriteMoveTime;

    private Unit _context;
    private Image _image;

    private Canvas _canvas;

    private void Awake() {
        _image = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable() {
        Game.Loop.OnMoveStarted += MoveStartHideOrShowIndicators;
    }

    private void OnDisable() {
        Game.Loop.OnMoveStarted -= MoveStartHideOrShowIndicators;
    }

    public void SetUnit(Unit unit) {
        _context = unit;

        SetupIndicators();
    }

    public void RemoveUnit() {
        _context = null;
    }

    public void SetSprite(Sprite sprite) {
        _image.sprite = sprite;
    }

    public void Move(Cage cage) {
        transform.SetParent(cage.View.transform);
        StartCoroutine(MoveRoutine());
    }

    public void ChangeIndicatorsVisibility(bool isVisible) {
        _moveIndicator.gameObject.SetActive(isVisible);
        _attackIndicator.gameObject.SetActive(isVisible);
    }

    protected virtual void SetupIndicators() {
        _moveIndicator.gameObject.SetActive(true);
        _moveIndicator.SetSkill(Unit.Mover);
        _attackIndicator.gameObject.SetActive(true);
        _attackIndicator.SetSkill(Unit.Attacker);
    }

    protected virtual void MoveStartHideOrShowIndicators() {
        if (_context != null) 
            ChangeIndicatorsVisibility(_context.Team == Game.CurrentTeam);
    }

    protected virtual IEnumerator MoveRoutine() {
        var moveVector = -transform.localPosition;
        transform.SetParent(_canvas.transform);
        float time = 0;
        while (time < _spriteMoveTime) {
            time += Time.deltaTime;
            transform.localPosition += moveVector * Time.deltaTime / _spriteMoveTime;
            yield return null;
        }
        if (_context != null) {
            transform.SetParent(_context.Cage.View.transform);
            transform.localPosition = Vector3.zero;
        } else {
            Destroy(gameObject);
        }
    }

}