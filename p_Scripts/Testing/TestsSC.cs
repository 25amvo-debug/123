using UnityEngine;

[System.Serializable]
public class TestsSC
{
    [Range(1, 12)]
    public int dificalt;
    public string BaseName;
    public string[] Answers;
    public MainController.Types testType;
    public int trueAnswer;
}
