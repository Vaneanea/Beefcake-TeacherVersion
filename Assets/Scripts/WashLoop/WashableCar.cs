using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles displaying the dirty car texture
public class WashableCar : MonoBehaviour {

    [SerializeField] private Texture2D dirtMaskTextureBase;
    [SerializeField] private Material material;

    private Texture2D dirtMaskTexture;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            WashCar();
        
    }

    private void WashCar() {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            Vector2 textureCoord = hit.textureCoord;

            //TODO: Implement painting on the dirt mask texture
        }

    }
}
