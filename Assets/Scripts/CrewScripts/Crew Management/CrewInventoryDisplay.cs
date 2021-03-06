using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CrewInventoryDisplay : MonoBehaviour {
    private List<CrewBeefCake> crew;

    private CrewUIManager uiManager;
    private int curItem;

    private void Start() {
        uiManager = GetComponent<CrewUIManager>();

        crew = CrewInventory.instance.crew;

        NotifyCurItemChange();
    }

    #region Display methods (attached to UI elements)
    // Attached to right arrow
    public void OnClickRight() {
        curItem = (curItem + 1) % crew.Count;
        NotifyCurItemChange();
    }

    // Attached to left arrow
    public void OnClickLeft() {
        curItem--;
        if (curItem < 0)
            curItem = crew.Count - 1;

        NotifyCurItemChange();
    }

    public void OnClickTrain() {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        CrewTrainingManager.curBeefCake = crew[curItem];
        CrewTrainingManager.trainStat = buttonName.Split(new char[] { ' ' })[0]; ;

        SceneManager.LoadScene("CrewTraining");
    }
    #endregion

    private void NotifyCurItemChange() {
        if (crew.Count == 1)
            uiManager.DisableArrows();

        uiManager.DisplayCurrentItem(crew[curItem]);
    }
}
