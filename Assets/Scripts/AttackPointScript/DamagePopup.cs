using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour {
    private TMP_Text textMesh;

    private float moveYDistance = 700f;
    private float moveSpeed = 1f;

    [SerializeField] private Color critColor;
    [SerializeField] private int critThreshold;

    private void Awake() {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Start() {
        LeanTween.moveY(gameObject, transform.position.y + moveYDistance, moveSpeed);

        // Fade out alpha over time
        Color color = textMesh.color;
        LeanTween.value(textMesh.gameObject, color.a, 0, moveSpeed)
        .setOnUpdate((float _value) => {
            color.a = _value;
            textMesh.color = color;
        });
    }

    public void Setup(int damageAmount) {
        textMesh.text = damageAmount.ToString();

        if (damageAmount > critThreshold)
            textMesh.color = critColor;
    }
}
