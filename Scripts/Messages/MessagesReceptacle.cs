using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagesReceptacle : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text DescText;
    public Image ActivePoint;

    private DataStorage dataStorage;
    private int count;
    private MainController mainController;
    public void Inizialize(DataStorage dT, int ct)
    {
        mainController = FindAnyObjectByType<MainController>();
        dataStorage = dT;
        count = ct;
        MessagesData message = dataStorage.messages[count];
        titleText.text = message.title;
        DescText.text = message.description;
        GetComponent<Image>().color = message.color;
        if (message.readed == false) { ActivePoint.gameObject.SetActive(true); }
        else { ActivePoint.gameObject.SetActive(false); }
    }
    public void Read()
    {
        dataStorage.messages[count].readed = true;
        ActivePoint.gameObject.SetActive(false);
        mainController.ReupdateMesText();
        mainController.fullMessageObj.SetActive(true);
        Transform im = mainController.fullMessageObj.transform.GetChild(0);
        im.transform.GetChild(0).GetComponent<TMP_Text>().text = titleText.text;
        im.transform.GetChild(1).GetComponent<TMP_Text>().text = dataStorage.messages[count].senderName;
        im.GetChild(2).GetComponent<TMP_Text>().text = DescText.text;
        im.transform.GetChild(3).GetComponent<TMP_Text>().text = dataStorage.messages[count].time;
        im.GetComponent<Image>().color = dataStorage.messages[count].color;
        
    }
    public void Delete()
    {
        dataStorage.messages.RemoveAt(count);
        FindAnyObjectByType<BlockSwitchController>().InizializateBlock(5);
        mainController.ReupdateMesText();
    }
}
