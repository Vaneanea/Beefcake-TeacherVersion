using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles interaction with the Client Info Pop-up
public class ClientInfoUI : MonoBehaviour
{
    //Managers
    private GameManager gm;

    private void Awake() {
        SetManager();
    }

    // TODO: Call this on swipe. For now it's called on click. 
    public void OnJobReject() {
        gm.OnJobRejected();
    }

    private void SetManager() {
        gm = FindObjectOfType<GameManager>();
    }
}
