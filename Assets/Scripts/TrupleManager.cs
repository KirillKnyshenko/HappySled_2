using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupleManager : MonoBehaviour
{
    public List<Transform> truples;

    [SerializeField] GameObject truple;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddTruple(Transform adder)
    {
        var newTruple = Instantiate(truple, truples[truples.Count - 1].transform);
        newTruple.transform.position = adder.position;
        truples.Add(newTruple.transform);
    }
}
