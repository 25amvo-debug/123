using UnityEngine;

public class WTestsSC : TestsSC
{
    public const float coef = 2f;
    public string rightAnswer;
    public WTestsSC()
    {
        rightAnswer = Answers[trueAnswer];
    }
}