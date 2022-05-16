using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainButtonController : MonoBehaviour {
    [Header("Mini-game variables")]
    [SerializeField] private Vector2 scaleTimeBounds;
    [SerializeField] private float minScale;
    [SerializeField] private float perfectThreshold;

    [Space(10)]
    [SerializeField] private int perfectAmount;

    private int perfectChain;

    public delegate void Trained(int trainAmount);
    public static event Trained OnTrain;

    private void Start() {
        perfectChain = 0;

        float scaleTime = Random.Range(scaleTimeBounds.x, scaleTimeBounds.y);
        LeanTween.scale(gameObject, new Vector3(minScale, minScale, minScale), scaleTime).setLoopPingPong();
    }

    public void OnClick() {
        float score = gameObject.transform.localScale.x;

        if (score > perfectThreshold) {
            perfectChain++;

            if (OnTrain != null)
                OnTrain(perfectAmount * perfectChain);
        } else 
            perfectChain = 0;
    }
}
