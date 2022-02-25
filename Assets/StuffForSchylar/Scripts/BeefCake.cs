using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeefCake", menuName = "ScriptableObjects/BeefCake", order = 1)]
public class BeefCake : ScriptableObject {
    public Mesh mesh;

    public string broName;
    public int level;

    public int cost;

    [Header("Stats")]
    public int strength;
    public int stamina;
    public int speed;
}
