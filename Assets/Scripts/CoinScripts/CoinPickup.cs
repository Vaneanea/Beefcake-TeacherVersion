using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    AudioSource myAudioSource;
    private Camera cam;
    public RaycastHit coinHit;
    private GameManager gm;
    private SoundEffectManager sem;
    private CoinManager coinM;
 

    // Start is called before the first frame update
    void Start()
    {
        SetGameManager();
        SetOtherManagers();
        cam = gm.cam;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))
        {
            //Ray rayM = cam.ScreenPointToRay(Input.GetTouch(0).position);
            Ray rayM = cam.ScreenPointToRay(Input.mousePosition);
            //Touch touch = Input.GetTouch(0);
            bool hit = Physics.Raycast(rayM, out coinHit, 100f);

            if (Input.GetButtonDown("Fire1") && hit /*|| (touch.phase == TouchPhase.Began) && hit*/)
            {
                if (coinHit.collider == gameObject.GetComponent<Collider>())
                {
                    //Debug.Log("clicky");
                    SoundEffectManager.Play("Coin");
                    coinM.AddToCoinsCaught();
                    Destroy(gameObject);

                }
            }
        }

    }

    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void SetOtherManagers()
    {
        coinM = FindObjectOfType<CoinManager>();
    }

}
