using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainButtonController : MonoBehaviour {
    [Header("Mini-game variables")]
    [SerializeField] private float scaleTime;
    [SerializeField] private float minScale;
    [SerializeField] private float perfectThreshold;

    [Space(10)]
    [SerializeField] private int perfectAmount;

    public delegate void Trained(int trainAmount);
    public static event Trained OnTrain;

    private void Start() {
        LeanTween.scale(gameObject, new Vector3(minScale, minScale, minScale), scaleTime).setLoopPingPong();
    }

    public void OnClick() {
        float score = gameObject.transform.localScale.x;

        if (score > perfectThreshold) {
            if (OnTrain != null)
                OnTrain(perfectAmount);
        }
    }
}
