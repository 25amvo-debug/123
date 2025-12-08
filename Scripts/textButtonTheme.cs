using UnityEngine;
using TMPro;

public class textButtonTheme : MonoBehaviour
{
    public enum TextOnButton {ПочнітьВивчати, ДаліБудуде, МожетеПовторити, Нон }
    public TextOnButton textOnButton;
    public void UpdateText()
    {
        switch (textOnButton)
        {
            case TextOnButton.ДаліБудуде:
                GetComponent<TMP_Text>().text = "Далі буде";
                break;
            case TextOnButton.МожетеПовторити:
                GetComponent<TMP_Text>().text = "Можете повторити";
                break;
            case TextOnButton.ПочнітьВивчати:
                GetComponent<TMP_Text>().text = "Почніть вивчати";
                break;
        }
    }
    private void Start()
    {
        UpdateText();
    }
}
