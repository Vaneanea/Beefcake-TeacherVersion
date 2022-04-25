using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobUIManager : MonoBehaviour {
    [Header("Display Fields")]
    [SerializeField] private GameObject slotParent;
    [SerializeField] private GameObject slotPrefab;

    public void ClearOldJobUI() {
        // Remove CarSlot UI object
        foreach (Transform child in slotParent.transform) {
            Destroy(child.gameObject);
        }
    }

    // Display generated CarType in pop-up UI
    public void AddCarSlot(CarTypeData carType, int starCount) {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(slotParent.transform);
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        CarSlot slot = obj.GetComponent<CarSlot>();
        slot.Set(carType, starCount);
    }
}
