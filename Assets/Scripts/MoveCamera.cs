using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offcetPostion;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;

    private void Start()
    {
        target = GameManager.player.transform;
        HandleTranslation();
    }

    private void Update()
    {
        HandleTranslation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.position + offcetPostion;
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
}