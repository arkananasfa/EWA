using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackVisual : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;

    private Vector3 flyDirection;

    private Transform _canvas;

    public void FlyTo(CageView cage) {
        flyDirection = cage.transform.position - transform.position;
        transform.up = flyDirection;
        _canvas = GetComponentInParent<Canvas>().transform;
        StartCoroutine(FlyRoutine());
    }

    private IEnumerator FlyRoutine() {
        float fullTime = flyDirection.magnitude / speed;
        transform.SetParent(_canvas);
        float time = 0;
        while (time < fullTime) {
            float deltaTime = Time.deltaTime;
            time += deltaTime;
            transform.position += deltaTime / fullTime * flyDirection;
            yield return null;
        }
        Destroy(gameObject);
    }

}