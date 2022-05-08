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
    [SerializeField] private int maxPaintDistance = 7;

    [Header("General")]
    [SerializeField] private float totalDirt; // Hardcoded for now, since it depends on texture

    private Texture2D dirtMaskTexture;
    private Vector2Int lastPixelPos;

    private float totalDirtRemoved;

    private WashUIManager uiManager;

    private void Awake() {
        SetVariables();
        MakeDirtMask();

        totalDirtRemoved = 0;
        uiManager.SetProgressMaxValue((int) totalDirt);
        uiManager.SetProgressValue((int) totalDirtRemoved);

        Input.simulateMouseWithTouches = false;
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
         WashCar(Input.mousePosition);
#endif

        if (Input.touchCount > 0)
            WashCar(Input.touches[0].position);
    }

    private void SetVariables() {
        uiManager = GameObject.Find("WashUIManager").GetComponent<WashUIManager>();
    }

    private void MakeDirtMask() {
        dirtMaskTexture = new Texture2D(dirtMaskTextureBase.width, dirtMaskTextureBase.height);
        dirtMaskTexture.SetPixels(dirtMaskTextureBase.GetPixels());
        dirtMaskTexture.Apply();

        material.SetTexture("_DirtMask", dirtMaskTexture);
    }

    #region Wash Car methods

    private void WashCar(Vector3 position) {
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("WashableCar");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out hit, layerMask)) {
            Vector2 textureCoord = hit.textureCoord;
           
            int pixelX = (int) (textureCoord.x * dirtMaskTexture.width);
            int pixelY = (int) (textureCoord.y * dirtMaskTexture.height);

            Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY);
            if (!CheckPixelDistance(paintPixelPosition))
                return;

            PaintDirtMask(pixelX, pixelY);

            dirtMaskTexture.Apply();
        }

        if (totalDirtRemoved >= totalDirt)
            uiManager.SetDebugText("CAR WASHED");
    }

    private bool CheckPixelDistance(Vector2Int curPixelPos) {
        int pixelDistance = Mathf.Abs(curPixelPos.x - lastPixelPos.x) + Mathf.Abs(curPixelPos.y - lastPixelPos.y);
        if (pixelDistance < maxPaintDistance)
            return false;

        lastPixelPos = curPixelPos;
        return true;
    }

    private void PaintDirtMask(int pixelX, int pixelY) {
        // Paint Dirt Brush texture in Dirt Mask
        int pixelXOffset = pixelX - (dirtBrush.width / 2);
        int pixelYOffset = pixelY - (dirtBrush.height / 2);
        for (int x = 0; x < dirtBrush.width; x++) {
            for (int y = 0; y < dirtBrush.height; y++) {
                Color pixelDirt = dirtBrush.GetPixel(x, y);
                Color pixelDirtMask = dirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);

                totalDirtRemoved += pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);

                dirtMaskTexture.SetPixel(
                    pixelXOffset + x,
                    pixelYOffset + y,
                    new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
                );
            }
        }

        uiManager.SetProgressValue((int) totalDirtRemoved);
    }
    #endregion
}
