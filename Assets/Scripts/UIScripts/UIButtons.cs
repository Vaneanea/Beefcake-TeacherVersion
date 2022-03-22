using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void WorkButton()
    {
      SceneManager.LoadScene("FixLoop");
    }
    public void MainMenuButton()
    {
      SceneManager.LoadScene("MainScene");
    }

    public void ShopButton()
    {
      SceneManager.LoadScene("CrewHire (Ziana)");
    }

    public void ActivateTempPauseMenu()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DeactivateTempPauseMenu()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
