using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("Bezier Transform")]
public class BezierTransform : MonoBehaviour
{
    [HideInInspector][SerializeField] private Transform _cacheTransform = null;
    public Transform CacheTransform
    {
        get
        {
            if (_cacheTransform == null)
                _cacheTransform = this.transform;
            return _cacheTransform;
        }
    }

    [Range(0f, 1f)]
    [HideInInspector][SerializeField] private float value = 0f;
    [HideInInspector][SerializeField] public Vector3 p1;
    [HideInInspector][SerializeField] public Vector3 p2;
    [HideInInspector][SerializeField] public Vector3 p3;
    [HideInInspector][SerializeField] public Vector3 p4;

    [HideInInspector][SerializeField] public bool playOnAwake = true;
    [HideInInspector][SerializeField] public bool autoLookAt = true;
    [HideInInspector][SerializeField] public float duration = 2.5f;
    [HideInInspector][SerializeField] public AnimationCurve animationCurve;

    private float _playTime = 0f;

    private void Awake()
    {
        if (animationCurve == null)
        {
            animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
    }

    private void OnEnable()
    {
        value = 0f;
        if (playOnAwake)
        {
            StopCoroutine("PlayAnimation");
            StartCoroutine("PlayAnimation");
        }
    }

    private void OnDisable()
    {
        StopCoroutine("PlayAnimation");
    }

    private IEnumerator PlayAnimation()
    {
        _playTime = 0f;
        while (_playTime < duration)
        {
            _playTime += Time.deltaTime;
            if (_playTime >= duration)
                _playTime = duration;

            var t = _playTime / duration;
            this.value = animationCurve.Evaluate(t);
            yield return null;
        }
        this.enabled = false;
    }

    public void FixedUpdate()
    {
        var currentPosition = BazierMath.Lerp(p1, p2, p3, p4, value);
        if (autoLookAt)
        {
            var nextPosition = BazierMath.Lerp(p1, p2, p3, p4, value + 0.01f);
            if (this.CacheTransform.parent)
            {
                this.CacheTransform.LookAt(nextPosition + this.CacheTransform.parent.position);
            }
            else
            {
                this.CacheTransform.LookAt(nextPosition);
            }
        }
        this.CacheTransform.localPosition = currentPosition;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 p1 = this.p1;
        Vector3 p2 = this.p2;
        Vector3 p3 = this.p3;
        Vector3 p4 = this.p4;

        if (CacheTransform.parent != null)
        {
            p1 += _cacheTransform.parent.position;
            p2 += _cacheTransform.parent.position;
            p3 += _cacheTransform.parent.position;
            p4 += _cacheTransform.parent.position;
        }

        GUI.color = Color.green;
        const int DRAW_COUNT = 25;
        for (float i = 0; i < DRAW_COUNT; i++)
        {
            float value_Before = i / DRAW_COUNT;
            Vector3 before = BazierMath.Lerp(p1, p2, p3, p4, value_Before);

            float value_After = (i + 1) / DRAW_COUNT;
            Vector3 after = BazierMath.Lerp(p1, p2, p3, p4, value_After);

            Gizmos.DrawLine(before, after);
        }
        GUI.color = Color.white;
    }
#endif
}
