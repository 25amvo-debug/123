using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class AnswerScript : MonoBehaviour
{
    public bool True = false;
    private Animator anim;
    private bool canBeTouced = true;
    public int answerRating;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Pessed()
    {
        if (canBeTouced)
        {
            transform.parent.GetComponent<Testrceptacle>().AddInList(answerRating, transform.GetChild(0).GetComponent<TMP_Text>().text);
            if (True)
            {
                anim.Play("truanim");
            }
            else
            {
                anim.Play("falseanim");
            }
            canBeTouced = false;
        }
    }
    public void OnPressedEnd()
    {
        transform.parent.GetComponent<Testrceptacle>().testingController.Next(answerRating, transform.GetChild(0).GetComponent<TMP_Text>().text);
        canBeTouced = true;
    }
}
