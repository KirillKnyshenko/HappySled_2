using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupleDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Truple")
        {
            GameManager.player.GetComponent<TrupleManager>().DeleteTruple(other.GetComponent<Transform>());
        }
    }
}
