using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        SetGameManager();
    }

    public void WorkButton()
    {
        if (gm.crewInventory.inventoryObject.transform.childCount <= 0)
        {
            gm.canvas.transform.GetChild(0).gameObject.SetActive(true);
            return;
        }
        else
        {
            SceneManager.LoadScene("ZianaDev");
        }

      
    }
    public void MainMenuButton()
    {
      SceneManager.LoadScene("MainScene");
    }

    public void ShopButton()
    {
      SceneManager.LoadScene("CrewHire");
    }

    public void ActivateTempPauseMenu()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void DeactivateTempPauseMenu()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ClosePopUp(GameObject obj)
    {
        obj.SetActive(false);
    }


    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }
}
