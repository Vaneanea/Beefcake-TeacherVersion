using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles displaying the dirty Car texture
public class WashableCar : MonoBehaviour {

    [Header("Dependencies")]
    [SerializeField] private Texture2D dirtMaskTextureBase;
    [SerializeField] private Material material;

    [Header("Brush Fields")]
    // [SerializeField] private Texture2D dirtBrush;
    [SerializeField] private int squareSize = 32;
    [SerializeField] private int maxPaintDistance = 7;

    private Texture2D dirtMaskTexture;
    private Vector2Int lastPixelPos;

    private void Awake() {
        MakeDirtMask();
    }

    private void Update() {
        if (Input.GetMouseButton(0))
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
            if (!CheckPixelDistance(paintPixelPosition))
                return;
            // Paint square in Dirt Mask
            int pixelXOffset = pixelX - (squareSize / 2);
            int pixelYOffset = pixelY - (squareSize / 2);
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

    private bool CheckPixelDistance(Vector2Int curPixelPos) {
        int pixelDistance = Mathf.Abs(curPixelPos.x - lastPixelPos.x) + Mathf.Abs(curPixelPos.y - lastPixelPos.y);
        if (pixelDistance < maxPaintDistance)
            return false;

        lastPixelPos = curPixelPos;
        return true;
    }
}
