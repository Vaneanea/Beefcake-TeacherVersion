using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSystemController : MonoBehaviour {
    [SerializeField] private float alphaDecreaseRate = 0.1f;
    [SerializeField] private Color startColor;

    private ParticleSystem bubbleSystem;

    private void Start() {
        bubbleSystem = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            if (hit.transform.gameObject == this.gameObject) {
                OnClick();
            }
        }
    }

    private void SetStartColor(Color newColor) {
        ParticleSystem.MainModule main = bubbleSystem.main;
        main.startColor = newColor;
    }

    private void OnClick() {
        startColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a - alphaDecreaseRate);
        SetStartColor(startColor);

        Debug.Log(startColor);
    }
}
