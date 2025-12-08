using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    private Animator anim;
    public string[] texts = new string[3];
    public MainController.Types types;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open(bool withAnim, bool isLeft = true)
    {
        GetComponent<Image>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        if (withAnim && isLeft)
        {
            anim.Play("SlideOpenAnim");
        }
        else if (withAnim && !isLeft)
        {
            anim.Play("SlideOpen2");
        }
    }
    public void Close(bool withAnim, bool isLeft = true)
    {
        if(withAnim && isLeft)
        {
            anim.Play("SlideClose");
        }
        else if(withAnim && !isLeft)
        {
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
