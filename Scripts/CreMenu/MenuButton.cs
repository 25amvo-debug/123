using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public bool isPlus;
    public Sprite plus;
    public Sprite minus;
    public UnityEvent onchange;

    public void Clicked()
    {
        isPlus = !isPlus;
        if(isPlus == true) {GetComponent<Image>().sprite = plus; }
        else { GetComponent<Image>().sprite = minus; }
        onchange?.Invoke();
    }
}
