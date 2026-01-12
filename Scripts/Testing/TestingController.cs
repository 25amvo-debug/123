using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class TestingController : MonoBehaviour
{
    public TestsSC[] bases;
    public Testrceptacle rceptacle;
    private int currentQuestion;
    public TMP_Text text;
    private float value;

    [HideInInspector] public List<int> S_curentMarks = new List<int>();
    [HideInInspector] public List<int> S_maxMarks = new List<int>();
    [HideInInspector] public List<string> S_rightString = new List<string>();
    [HideInInspector] public List<string> S_curentString = new List<string>();
    [HideInInspector] public List<float> S_currentTime = new List<float>();
    [HideInInspector] public List<MainController.Types> S_curentType = new List<MainController.Types>();


    public float GerMark()
    {
        float Max = 0;
        for (int i = 0; i < bases.Length; i++)
        {
            Max += bases[i].dificalt;
        }
        if (Max != 0)
        {
            float b =  value / Max;
            float o = 12*b;
            float M = Mathf.RoundToInt(o);
            return M;
        }
        else
        {
            return -1;
        }
    }
    private bool isRepet;
    public void StartTesting(bool rept)
    {
        S_currentTime.Clear();  
        isRepet = rept;
        currentQuestion = 0;
        int b = bases.Length;
        int a = 1;
        text.text = a.ToString() + " ²ç " + b.ToString();
        rceptacle._Initialization(bases[0]);
  
        rceptacle.testingController = GetComponent<TestingController>();
     }
    public void EndTesting()
    {
        float a = GerMark();
        FindAnyObjectByType<GlobalStatDataCollector>().ReupdateData(S_curentType.ToArray(), S_currentTime.ToArray(), a);
        value = 0;
        float l = 0;
        for(int i = 0; i < S_currentTime.Count; i++)
        {
            l += S_currentTime[i];
        }
        FindAnyObjectByType<MainController>().EndTestingYoohooo(a, l, isRepet);
    }  
    public void Next(int x, string y)
    {
        if (currentQuestion < bases.Length)
        {
            rceptacle.__base__ = bases[currentQuestion];
            value += x;
            
            currentQuestion++;
            if (currentQuestion < bases.Length)
            {
                rceptacle._Initialization(bases[currentQuestion]);
                int b = bases.Length ;
                int a = currentQuestion + 1;
                text.text = a.ToString() + " ²ç " + b.ToString();
            }
            else
            {
                EndTesting();
            }
        }
    }

}
