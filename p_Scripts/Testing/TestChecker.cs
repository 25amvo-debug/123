using UnityEngine;
using System.Data;
using System;

public class TestChecker : MonoBehaviour
{
    public TestsSC[] a;
    private void Awake()
    {
        for (int i = 0; i < a.Length; i++)
        {
            string b = a[i].BaseName;
            b = b.Replace("÷", "/");
            b = b.Replace(" ", "");
            b = b.Replace("×", "*");
            int res = Convert.ToInt32(new DataTable().Compute(b, ""));
            if (res != Convert.ToInt32(a[i].Answers[a[i].trueAnswer]))
            {
                Debug.Log(a[i].BaseName + " - Невірне " + res + " А не " + Convert.ToInt32(a[i].Answers[a[i].trueAnswer]) + " Level: " + a[i].dificalt);
            }
        }
        Debug.Log("Перевірка завершене");
    }
}
