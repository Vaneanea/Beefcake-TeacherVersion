using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Handles interaction and display for the Client Info Pop-up
public class ClientInfoUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private GameManager gm;
    private DynamicCarData dynamicCarData;

    [SerializeField] private float fadeDelay;
    [SerializeField] private float fadeSpeed;
    private List<Image> clientCardImages;

    #region Interaction vars
    [SerializeField] private float dragDistance;
    private Vector3 touchStartPos;
    private Vector3 popUpStartPos;
    #endregion

    private void Start() {
        gm = FindObjectOfType<GameManager>();
        SetClientCardImages();
    }

    private void OnEnable() {
        StartCoroutine(Fade());
    }

    // Sets the pop-up visuals based on the DynamicCarData
    public void OnInstantiate(DynamicCarData dynamicCarData) {
        this.dynamicCarData = dynamicCarData;

        transform.GetChild(0).GetComponent<Image>().sprite = dynamicCarData.clientVisuals.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

        // Adding stars
        for (int i = 0; i < dynamicCarData.starCount; i++) {
            var star = Resources.Load<GameObject>("Clients/ClientCardPrefabs/StarIcon");
            Instantiate(star, transform.GetChild(1).transform);
        }

        // Adding order requirements
        if (dynamicCarData.needWash == true)
            transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        if (dynamicCarData.needFix == true)
            transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
    }

    // Called on swipe 
    private void OnJobReject() {
        gm.OnJobRejected();
    }

    #region Interaction methods
    public void OnBeginDrag(PointerEventData eventData) {
        touchStartPos = Input.mousePosition;
        popUpStartPos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 distance = Input.mousePosition - touchStartPos;

        float newPosX = popUpStartPos.x + distance.x;
        transform.localPosition = new Vector3(newPosX, popUpStartPos.y, popUpStartPos.z);
    }

    public void OnEndDrag(PointerEventData eventData) {
        Vector3 distance = Input.mousePosition - touchStartPos;

        if (Mathf.Abs(distance.x) > dragDistance)
            OnJobReject();
        else 
            transform.localPosition = popUpStartPos;
    }
    #endregion

    #region Display methods
    private void SetClientCardImages() {
        Image[] clientCardImages = GetComponentsInChildren<Image>();
        var clientCardImagesList = clientCardImages.ToList();

        clientCardImagesList.Add(GetComponent<Image>());

        this.clientCardImages = clientCardImagesList;
    }

    public IEnumerator Fade() {
        yield return new WaitForSeconds(fadeDelay);

        float fadeAmount = 1f;
        while (fadeAmount > 0.0f) {
            foreach (Image img in clientCardImages) {
                Color color = GetComponent<Image>().color;
                fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

                img.color = new Color(color.r, color.g, color.b, fadeAmount);
            }
            yield return null;
        }

        gameObject.SetActive(false);
    }
    #endregion
}
