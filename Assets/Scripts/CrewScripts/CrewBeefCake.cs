using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "CrewBeefCake", menuName = "ScriptableObjects/CrewBeefCake", order = 1)]
// TODO: CrewBeefCake objects should not be created in Editor. Only for debugging CrewTraining. Remove this line ^^^. 
public class CrewBeefCake : ScriptableObject
{
    public int level;

    // Assumed that the {displayName} differs for all BeefCakes (used as ID)
    public string displayName;

    public int speed;
    public int strength;
    public int stamina;
    // TODO: Consider refactoring stats as a dictionary with string keys

    public int visualID;

    public GameObject characterPrefab;
    public Sprite headshot;
    public Color bgColor;

    public int currentStamina;
    public bool isFatigued = false;
    public bool isPlayer = false;
}
