using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PresentController : MonoBehaviour
{
    private Slide[] slides = new Slide[0];
    public int activeSlide;

    private bool canToLeft = true;
    private bool canToRight = true;
    public GameObject LeftButton;
    public GameObject RightButton;
    public void InitializationPresent(Slide[] sld)
    {
        slides = new Slide[sld.Length];
        for (int i = 0; i < sld.Length; i++)
        {
            slides[i]= Instantiate(sld[i]);
            slides[i].transform.SetParent(FindAnyObjectByType<Canvas>().transform, false);
        }
        for (int i = 0; i < slides.Length; i++)
        {
            if(i < activeSlide || i > activeSlide)
            {
                slides[i].Close(false);
            }
            else
            {
                slides[i].Open(false);
            }
        }
    }

    public void LeftSwipe()
    {
        Check();
        if (canToLeft)
        {
            slides[activeSlide].Close(true, true);
            activeSlide--;
            slides[activeSlide].Open(true, false);

        }
        Check();
    }

    public void RightSwipe()
    {
        Check();
        if (canToRight)
        {
            slides[activeSlide].Close(true, false);
            activeSlide++;
            slides[activeSlide].Open(true, true);

        }
        Check();
    }
    public void ShutDowningPresentation()
    {
        if (slides.Length > 0)
        {
            for (int i = 0; i < slides.Length; i++)
            {
                Destroy(slides[i].gameObject);
                slides[i] = null;
            }
            slides = new Slide[0];
        }
    }

    private void Check()
    {
        if (activeSlide == 0) { canToLeft = false; } else { canToLeft = true; }
        if (activeSlide == slides.Length - 1) { canToRight = false; } else { canToRight = true; }
        ChangeColor(RightButton, canToRight); ChangeColor(LeftButton, canToLeft);
    }
    public void ChangeColor(GameObject button, bool isWhite)
    {
        Color color1 = Color.blue;
        Color color2 = Color.blue;
        if (isWhite)
        {
             color1 = GetComponent<BlockSwitchController>().colorWhite;
             color2 = GetComponent<BlockSwitchController>().colorBlack;
        }
        else 
        {
            color1 = GetComponent<BlockSwitchController>().colorBlack;
            color2 = GetComponent<BlockSwitchController>().colorWhite;
        }
        button.GetComponent<Image>().color = color1;
        button.transform.GetChild(0).GetComponent<TMP_Text>().color = color2;
    }
}
