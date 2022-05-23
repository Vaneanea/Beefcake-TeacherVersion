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
            SceneManager.LoadScene("FixLoop");
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

    public void CrewButton() {
        SceneManager.LoadScene("CrewManagement");
    }

    public void StuffButton() {
        SceneManager.LoadScene("ShopScene");
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
