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

    public void DeleteTruple(Transform deleted)
    {
        var truples = GameManager.player.GetComponent<TrupleManager>().truples;
        var id = truples.FindIndex(x => x.GetInstanceID() == deleted.transform.GetInstanceID());
        if (id == 0)
        {
            UIManager.instance.Loose();
        }

        List<Transform> childs = deleted.transform.
        foreach (Transform child in deleted.transform)
        {
            var trupleInfo = child.GetComponent<TrupleInfo>();

            if (trupleInfo != null)
            {
                var truplesDog = trupleInfo.truplesDog;

                for (int i = 0; i < truplesDog.Count; i++)
                {
                    var bones = truplesDog[i].bones;

                    for (int j = 0; j < bones.Count; j++)
                    {
                        bones[j].isKinematic = false;
                    }
                }

                child.parent = null;
                child.GetChild(2).gameObject.SetActive(false);
                Destroy(child.GetComponent<TrupleInfo>());
                Destroy(child.gameObject, 10);
            }
        }

        var deletedInfo = deleted.GetComponent<TrupleInfo>();
        if (deletedInfo != null)
        {
            var deletedDog = deletedInfo.truplesDog;
            for (int i = 0; i < deletedDog.Count; i++)
            {
                var bones = deletedDog[i].bones;

                for (int j = 0; j < bones.Count; j++)
                {
                    bones[j].isKinematic = false;
                }
            }
        }

        truples.RemoveRange(id, truples.Count - id);
        deleted.parent = null;
        deleted.GetChild(2).gameObject.SetActive(false);
        Destroy(deleted.GetComponent<TrupleInfo>());
        Destroy(deleted.gameObject, 10);
    }
}
