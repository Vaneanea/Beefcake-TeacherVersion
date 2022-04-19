using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeBeefCake : MonoBehaviour {
    [Header("Position Fields")]
    [SerializeField] private Vector2 screenX;
    [SerializeField] private Vector2 worldX;

    [SerializeField] private Vector2 screenY;
    [SerializeField] private Vector2 worldZ;

    private void Update() {
        if (Input.touchCount <= 0) return; 

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
            float newX = Remap(touch.position.x, screenX.x, screenX.y, worldX.x, worldX.y);
            float newZ = Remap(touch.position.y, screenY.x, screenY.y, worldZ.x, worldZ.y);
            Vector3 newPos = new Vector3(newX, transform.position.y, newZ);

            transform.position = newPos;
        }
    }

    private float Remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
