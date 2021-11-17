using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupleAdder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Truple")
        {
            GameManager.player.GetComponent<TrupleManager>().AddTruple(transform);
            Destroy(transform.gameObject);
        }
    }
}