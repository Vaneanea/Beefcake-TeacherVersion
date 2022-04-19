using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashLoopManager : MonoBehaviour {
    private void Update() {
        HandleInput();
    }

   private void HandleInput() {
        if (!Input.GetMouseButtonDown(0)) return;

        // Cast raycast from click position forwards TODO: Convert to mobile input!
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            BubbleSystemController bubbleController = hit.transform.GetComponent<BubbleSystemController>();
            if (bubbleController != null) 
                bubbleController.OnClick();
        }
    }
} 
