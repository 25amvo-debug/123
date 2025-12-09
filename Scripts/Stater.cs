using UnityEngine;
using TMPro;
using System.Collections;

public class Stater : MonoBehaviour
{
    public TMP_Text text;
    public string BaseText;
    public void UpdateData(DataStorage dataStorage, bool afterTest = false)
    {
        string lv = "-";
        switch (FindAnyObjectByType<MainController>().returnLevel(dataStorage.levels[dataStorage.activeTheme-1]))
        {
            case DataStorage.Level.Low:
                lv = "прекрасно";
                break;
            case DataStorage.Level.Middle:
                lv = "чудово";
                break;
            case DataStorage.Level.High:
                lv = "вітаємо";
                break;
        }
        string markText = $"Ваша оцінка {dataStorage.LastMark} {lv}. \n Ваша середня оцінка {Mathf.Round(dataStorage.avarageMark)}. ";
        if (dataStorage.LastMark != -1 && afterTest == false)
        {
            text.text = markText;
        }
        else if(dataStorage.LastMark != -1 && afterTest == true)
        {
            StartCoroutine(PrintText(markText, 2f));
        }
        else
        {
            text.text = BaseText ;
        }
    }

    private IEnumerator PrintText(string inputText, float duration)
    {
        text.text = "";
        int Length = inputText.Length;
        float delay = duration / Length;
        for(int i = 0; i < Length; i++)
        {
            text.text += inputText[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
