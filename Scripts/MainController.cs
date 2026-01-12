using NaughtyAttributes.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public DataStorage dataStorage;
    public Testrceptacle testrceptacle;
    public enum Types { Other, MathIntroductionToAlgebra, MathIdentity, MathPowerWithNaturalExponent, 
        MathMonomialAndPolynomial, MathAddingAndSubtractingAndMultiplePolynomials, MathMOAPBAPFOP, 
        MathGroupingMethodTwoExpressions, MathSquareOfSumSquareOfDifference, MathConvertingPolynomialFactorization, }
    public List<List<TestsSC>> realTests =new List<List<TestsSC>>();
    public TestsSC[] Tests;
    public TMP_Text textTim_e;
    private ButtonTheme[] buttons;
    public AudioClip clip;
    public GameObject[] slides;
    public GameObject messagesRece;
    public Image activeMessageImage;
    public TMP_Text activeMessageText;
    public GameObject apMesObjAnim;
    public GameObject fullMessageObj;
    public Color[] colors;
    private Slide[] sld = new Slide[0];
    bool irRepet = false;
    public void Inizialization(int block, Types types, bool afterTest)
    {
        switch (block)
        {
            case 0:
                buttons = UnityEngine.Object.FindObjectsByType<ButtonTheme>(FindObjectsSortMode.None);
                foreach (ButtonTheme obj in buttons)
                {
                    int enumCode = (int)obj.typeButtonTheme;
                    if (enumCode < dataStorage.activeTheme)
                    {
                        obj.GetComponent<Animator>().Play("RepetThemeButtle");
                    }
                    else if (enumCode > dataStorage.activeTheme)
                    {
                        obj.GetComponent<Animator>().Play("ThemeButtle");
                    }
                    else
                    {
                        obj.GetComponent<Animator>().Play("New AnimationRun");
                    }
                    Vector3 p = new Vector3(0, 0, 0);
                    obj.GetComponent<RectTransform>().rotation = Quaternion.Euler(p);
                }
                break;
            case 2:
                
                GetComponent<BlockSwitchController>().HideButtons(false);
                GameObject.Find("ExitButton").SetActive(false);
                if ((int)types == dataStorage.activeTheme)
                {
                    irRepet = false;
                }
                else { irRepet = true; }
                    TestingController testingController = FindAnyObjectByType<TestingController>();
                testingController.rceptacle = Instantiate(testrceptacle.gameObject).GetComponent<Testrceptacle>();
                testingController.rceptacle.transform.SetParent(FindAnyObjectByType<Canvas>().gameObject.transform, false);
                testingController.rceptacle.TimeText = textTim_e;
                testingController.bases = GetTest(types);
                testingController.StartTesting(irRepet);
                break;
            case 3:
                FindAnyObjectByType<Stater>().UpdateData(dataStorage, afterTest);
                break;
            case 4:
                if(sld.Length >0 && bt.activeSelf == false)
                {
                    GetComponent<PresentController>().text.text = "";
                    GetComponent<PresentController>().InitializationPresent(sld);
                }
                break;
            case 5:
                if(dataStorage.messages.Count > 0)
                {
                    GetComponent<BlockSwitchController>().blocks[5].transform.GetChild(0).gameObject.SetActive(false);
                    float y = messagesRece.transform.position.y;
                    for (int i = 0; i < dataStorage.messages.Count; i++)
                    {
                        Vector3 pos = new Vector3(0, y, 0);
                        MessagesReceptacle mR = Instantiate(messagesRece, pos, Quaternion.identity).GetComponent<MessagesReceptacle>();
                        mR.transform.SetParent(GetComponent<BlockSwitchController>().blocks[5].gameObject.transform, false);
                        mR.Inizialize(dataStorage, i);
                        y -= 250;
                    }
                }
                else
                {
                    GetComponent<BlockSwitchController>().blocks[5].transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
        }
    }
    private void Start()
    {
       getSlides(false);   
    }
    public void StartLearning()
    {
        if(sld.Length > 0)
        {
            GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(2).gameObject.SetActive(true);
            FindAnyObjectByType<PresentController>().InitializationPresent(sld);
            GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private GameObject bt;
    private void a()
    {
        if (sld.Length ==0)
        {

            getSlides(true);
        }
    }
    public void getSlides(bool mes = true)
    {
        GetComponent<PresentController>().activeSlide = 0;
        GameObject but = GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(0).gameObject;
        but.SetActive(true);
        but.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ваша презентація готова!\r\nПочніть вивчати";
        if  (dataStorage.isFirst = false || dataStorage.activeTheme != 1)
        {
            if (mes&&GetComponent<BlockSwitchController>().activeBlock!=2) { SendMessages("Ваша презентація готова", "Пора вчитись", colors[5]); }
        }
        but.transform.GetChild(0).GetComponent<TMP_Text>().color = colors[9];
        but.GetComponent<Image>().color = colors[8];
        bt = but;
        List<Slide> sl = new List<Slide>();
        for(int i = 0; i < slides.Length; i++)
        {
            if (slides[i].GetComponent<Slide>().types == (Types)dataStorage.activeTheme)
            {
                sl.Add(slides[i].GetComponent<Slide>());
            }
        }
        if(sl.Count() == 0)
        {
            sl.Add(slides[23].GetComponent<Slide>());
        }
        sld = sl.ToArray();
    }
    public TestsSC[] GetTest(Types type)
    {
        TestsSC[] all = realTests[(int)type].ToArray();
        int minD = -1;
        int maxD = 99;
        if (irRepet)
        {
            switch (returnLevel(dataStorage.levels[(int)type]))
            {
                case DataStorage.Level.Low:
                    maxD = 4;
                    break;
                case DataStorage.Level.Middle:
                    minD = 5;
                    maxD = 8;
                    break;
                case DataStorage.Level.High:
                    minD = 9;
                    break;
            }
        }
        else
        {
            switch (returnLevel(dataStorage.levels[dataStorage.activeTheme-1]))
            {
                case DataStorage.Level.Low:
                    maxD = 4;
                    break;
                case DataStorage.Level.Middle:
                    minD = 5;
                    maxD = 8;
                    break;
                case DataStorage.Level.High:
                    minD = 9;
                    break;
            }
        }
            List<TestsSC> filtered = all.Where(t => t.dificalt >= minD && t.dificalt <= maxD).ToList();
        for (int i = filtered.Count - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (filtered[i], filtered[r]) = (filtered[r], filtered[i]);
        }
        return filtered.Take(8).ToArray();
    }
    public DataStorage.Level returnLevel(float num)
    {
        if (num == 0)
        {
            return DataStorage.Level.Low;
        }
        else if (num == 2)
        {
            return DataStorage.Level.High;
        }
        else
        {
            return DataStorage.Level.Middle;
        }
    }
    public void Skipped()
    {
        dataStorage.activeTheme--;
        skipdrept=true;
    }
    private bool skipdrept;
    public void EndTestingYoohooo(float mark, float time, bool rept)
    {
        sld = new Slide[0];
        GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(2).gameObject.SetActive(false);
        GameObject but = GetComponent<BlockSwitchController>().blocks[4].transform.GetChild(0).gameObject;
        but.SetActive(true);
        but.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ваша презентація ще неготова\n трохи зачекайте";
        but.transform.GetChild(0).GetComponent<TMP_Text>().color = colors[7];
        but.GetComponent<Image>().color = colors[0];
        float s = UnityEngine.Random.Range(10, 14);
        Invoke("a", s);
        if(mark > dataStorage.LastMark && mark >= 9)
        {
            SendMessages("Ви молодці", "Ваші оцінки стрімко ростуть!", colors[2]);
        }
        Destroy(FindAnyObjectByType<TestingController>().rceptacle.gameObject);
        float[] data = { dataStorage.avarageMark/12, mark/12, time/240, dataStorage.avaregeTime[dataStorage.activeTheme]/12 };
        string dt =   "Середня оцінка: " + data[0].ToString()+ " Оцінка: " + data[1].ToString() + " Час виконанна: " + data[2].ToString()+ "Середній час виконання: " + data[3].ToString();
        int studSkill = FindAnyObjectByType<Network>().StartNetwork(data);
        Debug.Log(dt);
        if (false==rept) { dataStorage.levels[dataStorage.activeTheme] = studSkill; dataStorage.activeTheme++; }
        dataStorage.LastMark = (int)Math.Round(mark);
        if (true==rept && true==skipdrept)
        {
            dataStorage.activeTheme++;
            skipdrept = false;
        }
        Debug.Log(rept);
        Debug.Log(skipdrept);

        GetComponent<BlockSwitchController>().InizializateBlock(3, afterText: true);
    }
    public void SendMessages(string title, string desc, Color color, string sender = "Neuro+", string time = "час невизначений")
    {
        MessagesData data = new MessagesData();
        data.title = title;
        data.description = desc;
        data.color = color;
        data.readed = false;
        data.senderName = sender;
        data.time = time;
        dataStorage.messages.Add(data);
        apMesObjAnim.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        apMesObjAnim.transform.GetChild(1).GetComponent<TMP_Text>().text = desc;
        apMesObjAnim.GetComponent<Image>().color = color;
        apMesObjAnim.GetComponent<Animator>().Play("mesAppearing");
        ReupdateMesText();
        if (GetComponent<BlockSwitchController>().activeBlock == 5) { GetComponent<BlockSwitchController>().InizializateBlock(5); }
    }
    public void ReupdateMesText()
    {
        int counOfUnReadedMessages = 0;
        for (int i = 0; i < dataStorage.messages.Count; i++)
        {
            if (dataStorage.messages[i].readed == false) { counOfUnReadedMessages++; }
        }
        if (counOfUnReadedMessages > 0)
        {
            activeMessageImage.gameObject.SetActive(true);
            activeMessageText.text = counOfUnReadedMessages.ToString();
        }
        else
        {
            activeMessageImage.gameObject.SetActive(false);
            activeMessageText.text = null;
        }
    }
    public void ExitOnMenu()
    {
        dataStorage.SaveToJson();
        SceneManager.LoadScene(0);
    }
    public void SetTestData(bool a)
    {
        if (a) {
            //SendMessages("Все добре", "У вас актуальна версія програми", colors[4]); 
        }
        else {  SendMessages("Почекайте", "У вас трохи застаріла версія програми", colors[3]); }
        var enumValues = Enum.GetValues(typeof(Types)).Cast<Types>().ToArray();
        foreach (var ev in enumValues)
        {
            var row = Tests.Where(x => x.testType == ev).ToList();
            realTests.Add(row);
        }
    }
    public void CLosefullMessageObj()
    {
        fullMessageObj.SetActive(false);
    }
}
