using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OLD_UIButtonScripts : MonoBehaviour
{
    public void StartButton() {

        SceneManager.LoadScene("FixCarScene");

    }

    public void RestartButton()
    {
       
        SceneManager.LoadScene("StartScreen");

    }
}
