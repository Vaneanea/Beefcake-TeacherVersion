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

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Sponge")) OnClean();
    }

    public void OnClean() {
        startColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a - alphaDecreaseRate);
        SetStartColor(startColor);

        if (startColor.a <= 0.0f)
            StartCoroutine(BubbleCleanAfterSeconds(bubbleSystem.main.duration));
    }
    private void SetStartColor(Color newColor) {
        ParticleSystem.MainModule main = bubbleSystem.main;
        main.startColor = newColor;
    }

    IEnumerator BubbleCleanAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);

        // TODO: Notify something to update the progress bar. 

        Destroy(gameObject);
    }
}