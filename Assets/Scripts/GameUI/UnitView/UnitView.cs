using System.Collections;
using Unity.VisualScripting;
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

    [SerializeField]
    private float _spriteDieTime;

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

    public void Move(CageView cage, float wait, float time) {
        StartCoroutine(MoveRoutine(cage, wait, time));
    }

    public virtual void UpdateStatus() {
        if (_context != null) {
            UpdateHP((float)(_context.HP / _context.MaxHP));
        } else {
            Kill();
        }
    }

    protected void UpdateHP(float percent) {
        //...
    }

    protected void Kill() {
        StartCoroutine(DeathRoutine());
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

    protected virtual IEnumerator MoveRoutine(CageView cage, float wait, float allTime) {
        yield return new WaitForSeconds(wait);
        transform.SetParent(cage.transform);
        var moveVector = -transform.localPosition;
        transform.SetParent(_canvas.transform);
        float time = 0;
        while (time < allTime) {
            time += Time.deltaTime;
            transform.localPosition += moveVector * Time.deltaTime / allTime;
            yield return null;
        }
        if (_context != null) {
            transform.SetParent(_context.Cage.View.transform);
            transform.localPosition = Vector3.zero;
        }
    }

    protected virtual IEnumerator DeathRoutine() {
        float time = 0;
        while (time < _spriteDieTime) {
            time += Time.deltaTime;
            _image.ChangeAlpha(Mathf.Lerp(1f, 0f, time/_spriteDieTime));
            yield return null;
        }
        Destroy(gameObject);
    }

}