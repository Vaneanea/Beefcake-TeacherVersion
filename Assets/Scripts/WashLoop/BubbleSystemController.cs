using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSystemController : MonoBehaviour {
    [SerializeField] private float alphaDecreaseRate;
    [SerializeField] private Color startColor;

    private ParticleSystem bubbleSystem;

    private void Start() {
        bubbleSystem = GetComponent<ParticleSystem>();
    }

    public void OnClick() {
        startColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a - alphaDecreaseRate);
        SetStartColor(startColor);

        if (startColor.a <= 0.0f)
            Destroy(gameObject); // TODO: Destroy after some time passes instead.
    }
    private void SetStartColor(Color newColor) {
        ParticleSystem.MainModule main = bubbleSystem.main;
        main.startColor = newColor;
    }
}
