using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class AnimatedObject : MonoBehaviour {

    public float Speed = 1000;

    private static AnimatedObject Prefab {
        get {
            if (_prefab == null) {
                _prefab = Resources.Load<AnimatedObject>("AnimatedObject");
                if (_prefab == null)
                    throw new Exception("Missing AnimatedObject prefab.");
            }
            return _prefab;
        }
    }
    private static AnimatedObject _prefab;

    public Action<Sprite> SetSprite;
    public Action<bool> SetSpriteActive;

    private float _waitTime;
    private float _time;

    private Transform _lastTransform;

    private AnimationContainer _container;

    public static AnimatedObject Create(Image image) {
        var ao = image.gameObject.AddComponent<AnimatedObject>();
        ao.SetSprite = s => image.sprite = s;
        ao.SetSpriteActive = b => image.enabled = b;
        return ao;
    }

    public static AnimatedObject Create(UnitView unitView) {
        var ao = unitView.gameObject.AddComponent<AnimatedObject>();
        ao.SetSprite = s => unitView.SetSprite(s);
        ao.SetSpriteActive = b => unitView.GetComponent<Image>().enabled = b;
        return ao;
    }

    public static AnimatedObject CreateProjectileAt(CageView cageView, string code) {
        var ao = Instantiate(Game.SpritesExtractor.GetAttackVisual(code), cageView.transform).GetComponent<AnimatedObject>(); ;
        ao.SetSprite = s => ao.GetComponent<Image>().sprite = s;
        ao.SetSpriteActive = b => ao.GetComponent<Image>().enabled = b;
        return ao;
    }

    public static AnimatedObject CreateAt(UnitView unitView) {
        var ao = Instantiate(Prefab, unitView.transform);
        ao.SetSprite = s => ao.gameObject.GetComponent<Image>().sprite = s;
        ao.SetSpriteActive = b => ao.gameObject.GetComponent<Image>().enabled = b;
        return ao;
    }

    public static AnimatedObject CreateAt(CageView cageView) {
        var ao = Instantiate(Prefab, cageView.transform);
        ao.SetSprite = s => ao.gameObject.GetComponent<Image>().sprite = s;
        ao.SetSpriteActive = b => ao.gameObject.GetComponent<Image>().enabled = b;
        return ao;
    }

    public AnimatedObject SetSkillIconAsSprite(string code) {
        SetSprite(Game.SpritesExtractor.GetSkillSprite(code));
        return this;
    }

    public void LikeAttack(Cage to, UnitView defenderView, float speed = -1f) {
        if (speed > 0f) {
             SetSpeed(speed)
            .MoveDefineTime(to.View)
            .AfterThat().ReleaseSequence()
            .UpdateUnitStatus(defenderView)
            .Kill();
        } else {
             MoveDefineTime(to.View)
            .AfterThat().ReleaseSequence()
            .UpdateUnitStatus(defenderView)
            .Kill();
        }
    }

    public void LikeAttack(Cage to, float speed = -1f) {
        if (speed > 0f) {
            SetSpeed(speed)
           .MoveDefineTime(to.View)
           .AfterThat().ReleaseSequence()
           .Kill();
        } else {
            MoveDefineTime(to.View)
           .AfterThat().ReleaseSequence()
           .Kill();
        }
    }

    public AnimatedObject InContainer(AnimationContainer container) {
        _container = container;
        return this;
    }

    public AnimatedObject InSeconds(float time) {
        _time = time;
        return this;
    }

    public AnimatedObject AfterThat() {
        _waitTime += _time;
        _time = 0f;
        return this;
    }

    public AnimatedObject WaitFor(float time) { 
        return InSeconds(time).AfterThat();
    }

    public AnimatedObject AfterThatWaitFor(float time) {
        return AfterThat().WaitFor(time);
    }

    public AnimatedObject ReleaseSequence() {
        StartCoroutine(ReleaseRoutine(_waitTime));
        return this;
    }

    public AnimatedObject End() {
        StartCoroutine(KillRoutine(_waitTime + _time));
        return this;
    }

    public AnimatedObject Kill() {
        StartCoroutine(KillRoutine(_waitTime + _time, true));
        return this;
    }

    public AnimatedObject MoveUnit(UnitView unitView, CageView target) {
        unitView.Move(target, _waitTime, _time);
        return this;
    }

    public AnimatedObject UpdateUnitStatus(UnitView unitView) {
        StartCoroutine(UpdateUnitStatusRoutine(_waitTime, unitView));
        return this;
    }

    public AnimatedObject SetSpeed(float speed) {
        Speed = speed;
        return this;
    }

    public AnimatedObject MoveTo(CageView cageView) {
        StartCoroutine(MoveRoutine(cageView.transform, _waitTime, _time));
        return this;
    }

    public AnimatedObject MoveDefineTime(CageView cageView) {
        StartCoroutine(MoveRoutine(cageView.transform, _waitTime));
        return this;
    }

    protected virtual IEnumerator MoveRoutine(Transform parentT, float wait, float allTime) {
        yield return new WaitForSeconds(wait);
        transform.SetParent(parentT);
        var moveVector = -transform.localPosition;
        transform.up = moveVector;
        transform.SetParent(GetComponentInParent<Canvas>().transform);
        float time = 0;
        while (time < allTime) {
            time += Time.deltaTime;
            transform.localPosition += moveVector * Time.deltaTime / allTime;
            yield return null;
        }
    }

    protected virtual IEnumerator MoveRoutine(Transform parentT, float wait) {
        float allTime = 0;
        if (_lastTransform == null) {
            transform.SetParent(parentT);
            var moveVector = -transform.localPosition;
            allTime = moveVector.magnitude / Speed;
            _time = allTime;
            _lastTransform = parentT;
            transform.SetParent(GetComponentInParent<Canvas>().transform);
        } else {
            var currentParent = transform.parent;
            transform.SetParent(_lastTransform);
            transform.localPosition = Vector3.zero;
            transform.SetParent(parentT);
            var moveVector = -transform.localPosition;
            transform.SetParent(currentParent);
            allTime = moveVector.magnitude / Speed;
            _time = allTime;
            _lastTransform = parentT;
        }
        yield return MoveRoutine(parentT, wait, allTime);
    }

    private IEnumerator ReleaseRoutine(float wait) {
        yield return new WaitForSeconds(wait);
        _container.Release();
    }

    private IEnumerator KillRoutine(float wait, bool kill = false) {
        yield return new WaitForSeconds(wait);
        if (kill) {
            Destroy(gameObject);
        } else {
            Destroy(this);
        }
    }

    private IEnumerator UpdateUnitStatusRoutine(float wait, UnitView unitView) {
        yield return new WaitForSeconds(wait - 0.1f);
        if (unitView != null)
            unitView.UpdateStatus();
    }

}