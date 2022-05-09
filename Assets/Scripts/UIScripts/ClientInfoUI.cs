using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// Handles interaction with the Client Info Pop-up
public class ClientInfoUI : MonoBehaviour {
    [SerializeField] private float fadeDelay = 2f;
    [SerializeField] private float fadeSpeed = 1f;
    private List<Image> clientCardImages;

    private DynamicCarData dynamicCarData;

    private GameManager gm;

    private void Start() {
        SetManager();
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

    // TODO: Call this on swipe. For now it's called on click. 
    public void OnJobReject() {
        gm.OnJobRejected();
    }

    private void SetManager() {
        gm = FindObjectOfType<GameManager>();
    }

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
