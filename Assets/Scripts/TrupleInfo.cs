using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dog
{
    public Transform dog, backPoint;
    public LineRenderer dogLine;
}
public class TrupleInfo : MonoBehaviour
{
    public List<Dog> truplesDog;

    void Start()
    {
        // Work woth LineRendere
        for (int i = 0; i < truplesDog.Count; i++)
        {
            truplesDog[i].dogLine.SetPosition(0, truplesDog[i].dog.GetChild(0).position);
            truplesDog[i].dogLine.SetPosition(1, truplesDog[i].backPoint.position);   
        }
    }
}
