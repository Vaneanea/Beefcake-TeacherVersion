using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeefCakeVisualData", menuName = "ScriptableObjects/BeefCakeVisualData", order = 2)]
public class BeefCakeVisualData : ScriptableObject
{
    public int visualID;

    public Sprite headshot;
    public GameObject characterPrefab;
    public GameObject shopPose;
    public Color bgColor;
}
