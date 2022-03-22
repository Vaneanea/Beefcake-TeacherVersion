using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Component of a shop slot, which holds an instance of an ItemData with extra fields
 * Handles player interaction (buying item) & displaying a shop slot
 */
public class ShopItem : MonoBehaviour {
    [HideInInspector] public ItemData source;

    // TODO: Update this var based on current stage/other parameters
    private bool isAvailable;

    // TODO: Add a stack size? Can the player buy more than one of each item? What happens when item is sold out?

    #region UI variables
    [SerializeField] private Image iconSprite;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costValue;
    #endregion

    public void Set(ItemData item) {
        source = item;

        isAvailable = true;

        // Set UI variables
        iconSprite.sprite = item.icon;
        nameText.text = item.displayName;
        costValue.text = item.cost.ToString("n0");
    }

    #region Item Buy Methods

    // Attached to Buy Button on Item Slot object
    public void OnItemBuy() {
        // TODO: Check if Item can be bought

        ItemInventory.instance.Add(this);
        isAvailable = false;
    }
    #endregion  
}
