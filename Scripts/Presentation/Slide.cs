using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slide : MonoBehaviour
{
    private Animator anim;
    public string[] texts = new string[3];
    public MainController.Types types;
    private PresentController presentController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        presentController = FindAnyObjectByType<PresentController>();
    }
    public void Open(bool withAnim, bool isLeft = true)
    {
        GetComponent<Image>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        if (withAnim && isLeft)
        {
            StartCoroutine(PlayAndWait("SlideOpenAnim"));
        }
        else if (withAnim && !isLeft)
        {
            StartCoroutine(PlayAndWait("SlideOpen2"));
        }
    }

    IEnumerator PlayAndWait(string animName)
    {
        anim.Play(animName);
        float time = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
        presentController.text.text = "";
        int theme = presentController.GetComponent<MainController>().dataStorage.activeTheme-1;
        StartCoroutine(presentController.PrintText(texts[presentController.GetComponent<MainController>().dataStorage.levels[theme]], 2f));
    }
    public void Close(bool withAnim, bool isLeft = true)
    {
        if(withAnim && isLeft)
        {
            presentController.text.text = "";
            StopAllCoroutines();
            anim.Play("SlideClose");
        }
        else if(withAnim && !isLeft)
        {
            presentController.text.text = "";
            StopAllCoroutines();
            anim.Play("SlideClose2");
        }
        else if (!withAnim)
        {
            RealClose();
        }
    }
    public void RealClose()
    {
        GetComponent<Image>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
