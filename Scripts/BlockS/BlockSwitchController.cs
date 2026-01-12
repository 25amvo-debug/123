using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI    ;

public class BlockSwitchController : MonoBehaviour
{
    public GameObject[] blocks;
    public int activeBlock;
    public BlockButton[] blockButtons;
    public Color colorWhite;
    public Color colorBlack;
    private GameObject exitButton;
    public TMP_Text welcomeText;
    public List<string> welcomeTexts = new List<string>();

    public void HideButtons(bool state)
    {
        foreach (BlockButton buton in blockButtons)
        {
            if (buton != null)
            {
                buton.gameObject.SetActive(state);
            }
        }
    }
    private IEnumerator PrintText(string inputText, float duration)
    {
        welcomeText.text = "";
        int Length = inputText.Length;
        float delay = duration / Length;
        for (int i = 0; i < Length; i++)
        {
            welcomeText.text += inputText[i];
            yield return new WaitForSeconds(delay);
        }
    }
    void printWelcomeText()
    {

        int a = Random.Range(0, welcomeTexts.Count);
        string t = welcomeTexts[a];
        StartCoroutine(PrintText(t, 2f));
    }
    private void Start()
    {
        printWelcomeText();
        exitButton = GameObject.Find("ExitButton");
        InizializateBlock(activeBlock);
        GetComponent<MainController>().ReupdateMesText();

    }
    public void SwitchBlockState(int block, bool state)
    {
        blocks[block].SetActive(state);
    }
    public void ClearMessages()
    {
        Transform[] child = blocks[5].GetComponentsInChildren<Transform>();
        if (child.Length > 0)
        {
            for (int i = 0; i < child.Length; i++)
            {
                if (child[i].GetComponent<MessagesReceptacle>() != null)
                {
                    Destroy(child[i].gameObject);
                }
            }
        }
    }
    public void InizializateBlock(int block, MainController.Types types = MainController.Types.Other, bool afterText = false)
    {
        if (block != 2 || (int)types <= GetComponent<MainController>().dataStorage.activeTheme)
        {
            activeBlock = block;
            HideButtons(true);
            if(activeBlock == 0) { welcomeText.gameObject.SetActive(true); }
            else {welcomeText.gameObject.SetActive(false);  }
            FindAnyObjectByType<PresentController>().ShutDowningPresentation();
            ClearMessages();
            exitButton.SetActive(true);
            foreach (BlockButton buton in blockButtons)
            {
                if (buton != null)
                {
                    buton.GetComponent<Image>().color = colorWhite;
                    buton.transform.GetChild(0).GetComponent<Image>().color = colorBlack;
                }
            }
            if (activeBlock != 2)
            {
                blockButtons[activeBlock].GetComponent<Image>().color = colorBlack;
                blockButtons[activeBlock].transform.GetChild(0).GetComponent<Image>().color = colorWhite;
            }
            for (int i = 0; i < blocks.Length; i++)
            {
                SwitchBlockState(i, false);
            }
            SwitchBlockState(activeBlock, true);
            GetComponent<MainController>().Inizialization(activeBlock, types, afterText);
        }
    }
}
