using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;



[CreateAssetMenu(fileName = "New Car")]
public class Car : ScriptableObject
{
    //car stages
    public GameObject[] carStates = new GameObject[3];

    public int firstStageHitsNeeded, secondStageHitsNeeded;

    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

}







//[CreateAssetMenu(menuName = "Cars/Pizza")]
//public class PizzaCar : Car
//{
    

//}




