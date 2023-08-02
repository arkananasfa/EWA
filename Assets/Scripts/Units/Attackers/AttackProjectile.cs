using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class AttackProjectile : MonoBehaviour {

    [SerializeField]
    private float _flyTime;

    [SerializeField]
    private AudioClip _audioClip;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public void Init(CageView start, CageView end) {
        transform.up = end.transform.position - start.transform.position;
        if (_audioClip != null) { 
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = _audioClip;
            audioSource.PlaySoundThroughManager();
        }
        _startPosition = start.transform.position;
        _endPosition = end.transform.position;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine() {
        float time = 0;
        while (time < _flyTime) {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, time);
            yield return null;
        }
        Destroy(gameObject);
    }

}