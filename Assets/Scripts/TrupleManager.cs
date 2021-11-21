using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupleManager : MonoBehaviour
{
    public List<Transform> truples;

    [SerializeField] GameObject truple;
    public AnimationCurve trupleCurve;
    public float maxRotate, trupleAngle;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.gameStage == GameStage.Game)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                trupleAngle = (Input.GetAxis("Mouse X") * maxRotate) / truples.Count;

                for (int i = truples.Count - 1; i >= 0; i--)
                {

                    if (!(truples[i].transform.position.x >= 5.2f || truples[i].transform.position.x <= -5.2f))
                    {
                        truples[i].rotation = Quaternion.Lerp(truples[i].transform.rotation, Quaternion.Euler(new Vector3(0, trupleAngle, 0)), Time.deltaTime);
                    }
                    else
                    {
                        truples[i].rotation = Quaternion.Lerp(truples[i].transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime);
                    }
                }
            }
            else
            {
                for (int i = 0; i < truples.Count; i++)
                {
                    truples[i].rotation = Quaternion.Lerp(truples[i].transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime);
                }
            }
        }
    }

    public void AddTruple(Transform addered)
    {
        var newTruple = Instantiate(truple, truples[truples.Count - 1].transform);
        newTruple.transform.position = addered.position;
        truples.Add(newTruple.transform);
    }

    public void DeleteTruple(Transform trupleDel)
    {
        var allTruples = trupleDel.GetComponentsInChildren<TrupleInfo>();
        foreach (var item in allTruples)
        {
            truples.Remove(item.transform);
            item.transform.parent = null;

            item.enabled = false;
            StartCoroutine(disableDogs(10, item.gameObject));

            for (int i = 0; i < item.truplesDog.Count; i++)
            {
                item.truplesDog[i].dogLine.transform.parent.gameObject.SetActive(false);
                item.truplesDog[i].dogAnimator.enabled = false;
                foreach (var rb in item.truplesDog[i].bones)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(200, item.transform.position, 20);
                }
            }
        }
        if (truples.Count == 0)
        {
            UIManager.instance.Loose();
        }
    }

    IEnumerator disableDogs(float seconds, GameObject truple)
    {
        yield return new WaitForSeconds(seconds);
        truple.SetActive(false);
    }
}
