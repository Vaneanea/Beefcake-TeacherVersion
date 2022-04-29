using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles displaying the dirty Car texture
public class WashableCar : MonoBehaviour {

    [Header("Dependencies")]
    [SerializeField] private Texture2D dirtMaskTextureBase;
    [SerializeField] private Material material;

    [Header("Brush Fields")]
    [SerializeField] private Texture2D dirtBrush;

    private Texture2D dirtMaskTexture;

    private void Awake() {
        MakeDirtMask();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            WashCar();
        
    }

    private void MakeDirtMask() {
        dirtMaskTexture = new Texture2D(dirtMaskTextureBase.width, dirtMaskTextureBase.height);
        dirtMaskTexture.SetPixels(dirtMaskTextureBase.GetPixels());
        dirtMaskTexture.Apply();

        material.SetTexture("_DirtMask", dirtMaskTexture);
    }

    private void WashCar() {
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("WashableCar");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, layerMask)) {
            Vector2 textureCoord = hit.textureCoord;

            int pixelX = (int) (textureCoord.x * dirtMaskTexture.width);
            int pixelY = (int) (textureCoord.y * dirtMaskTexture.height);

            Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY);

            // Paint square in Dirt Mask
            int squareSize = 32;
            int pixelXOffset = pixelX - (dirtBrush.width / 2);
            int pixelYOffset = pixelY - (dirtBrush.height / 2);

            for (int x = 0; x < squareSize; x++) {
                for (int y = 0; y < squareSize; y++) {
                    dirtMaskTexture.SetPixel(
                        pixelXOffset + x,
                        pixelYOffset + y,
                        Color.black
                    );
                }
            }

            dirtMaskTexture.Apply();
        }
    }
}
