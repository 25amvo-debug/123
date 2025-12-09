using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Testrceptacle : MonoBehaviour
{
    [HideInInspector] public TestsSC __base__;
    [HideInInspector]
    public TestingController testingController;
    public Button[] answers;
    private float Time;
    public TMP_Text TimeText;
    public void Update()
    {
        if (TimeText != null) { TimeText.text = Time.ToString(); }
    }
    IEnumerator Timer()
    {
        while (true)
        {
            Time+=0.1f;
            Time = Mathf.Round(Time*10f)/10;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void _Initialization(TestsSC _base)
    {
        StartCoroutine(Timer());
        __base__ = _base;
        GetComponent<TMP_Text>().text = _base.BaseName;
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponentInChildren<TMP_Text>().text = _base.Answers[i];
            answers[i].GetComponent<AnswerScript>().True = false;
            answers[i].GetComponent<AnswerScript>().answerRating = 0;
        }
        answers[_base.trueAnswer].GetComponent<AnswerScript>().True = true;
        answers[_base.trueAnswer].GetComponent<AnswerScript>().answerRating = _base.dificalt;
    }
    public void AddInList(int x, string y)
    {
        testingController.S_curentType.Add(__base__.testType);
        testingController.S_curentMarks.Add(x);
        testingController.S_maxMarks.Add(__base__.dificalt);
        testingController.S_rightString.Add(__base__.Answers[__base__.trueAnswer]);
        testingController.S_curentString.Add(y);
        testingController.S_currentTime.Add(Time);
        StopCoroutine(Timer());
        StopAllCoroutines();
        Time = 0;
    }
    private void Start()
    {
       // _Initialization(__base__);
    }
}
