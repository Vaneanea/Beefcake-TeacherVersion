using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScaleTween : MonoBehaviour {
    [SerializeField] private float tweenTime = 0.5f;

    private Vector3 initialScale;

    private void OnEnable() {
        initialScale = gameObject.transform.localScale;

        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), tweenTime).setEase(LeanTweenType.easeInBounce).setOnComplete(DisableMe);        
    }

    private void DisableMe() {
        gameObject.transform.localScale = initialScale;
        gameObject.SetActive(false);
    }
}
