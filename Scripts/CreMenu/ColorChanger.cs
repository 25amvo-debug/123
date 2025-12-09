using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color colorGr;
    public Color colorRd;
    private MenuButton[] menuButtons;
    public float transDuration;
    public DataStorage dataStorage;
    [HideInInspector] public int num;
    public void ChangeMenuColour()
    {
        num++;
        if(0 >  30)
        {
            dataStorage.Rest();
        }
        menuButtons = Object.FindObjectsByType<MenuButton>(FindObjectsSortMode.None);
        if (menuButtons[0].isPlus == menuButtons[1].isPlus)
        {
            StartCoroutine(changeColor(colorGr, transDuration));
        }
        else
        {
            StartCoroutine (changeColor(colorRd, transDuration));
        }
    }

    IEnumerator changeColor(Color targetColor, float duration)
    {
        float time = 0;
        Color startColor = GetComponent<Camera>().backgroundColor;
        while (time < duration)
        {
            GetComponent<Camera>().backgroundColor = Color.Lerp(startColor, targetColor, time/ duration);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<Camera>().backgroundColor = targetColor;
    }
}
