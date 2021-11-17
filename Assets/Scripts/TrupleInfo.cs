using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dog
{
    public Transform dog, backPoint;
    public LineRenderer dogLine;
    public Animator dogAnimator;
}

public class TrupleInfo : MonoBehaviour
{
    public List<Dog> truplesDog;

    void Update()
    {
        transform.eulerAngles = new Vector3();
  
        for (int i = 0; i < truplesDog.Count; i++)
        {
            // Work with LineRenderer
            truplesDog[i].dogLine.SetPosition(0, truplesDog[i].dog.GetChild(0).position);
            if (truplesDog[i].backPoint)
            {
                truplesDog[i].dogLine.SetPosition(1, truplesDog[i].backPoint.position);
            }
            else
            {
                var parentDog = truplesDog[i].dog.parent.parent.GetComponent<TrupleInfo>().truplesDog[i].dog;
                truplesDog[i].dogLine.SetPosition(1, parentDog.GetChild(0).position);
                truplesDog[i].backPoint = parentDog.GetChild(0);
            }

            if (GameManager.gameStage == GameStage.Game)
            {
                // Animation
                truplesDog[i].dogAnimator.SetLayerWeight(1, truplesDog[i].dogAnimator.GetLayerWeight(1) + Time.deltaTime);

                //Moving
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 5), Time.deltaTime);
            }
        }
    }
}
