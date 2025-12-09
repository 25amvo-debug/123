using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollController : MonoBehaviour
{
    public InputActionReference scrollAction;
    public RectTransform themeButtons;
    public RectTransform messeGess;
    public float speed;

    private void OnEnable()
    {
        scrollAction.action.Enable();
    }

    private void OnDisable()
    {
        scrollAction.action.Disable();
    }
    private BlockSwitchController blockSwitchController;
    private RectTransform scrollThemeControll;
    private RectTransform buttons;
    private GameObject fulMesButton;
    private DataStorage dataStorage;
    private RectTransform Checker327;
    private void Awake()
    {
        Checker327 = GameObject.Find("checker327").GetComponent<RectTransform>();
        blockSwitchController = GetComponent<BlockSwitchController>();
        dataStorage = blockSwitchController.GetComponent<MainController>().dataStorage;
        scrollThemeControll = themeButtons.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>();
        buttons = GameObject.Find("Buttons").GetComponent<RectTransform>();
        fulMesButton = GetComponent<MainController>().fullMessageObj;
    }
    void Update()
    {
        Vector2 delta = scrollAction.action.ReadValue<Vector2>();
        if (blockSwitchController.activeBlock == 0)
        {
            Vector2 pos = themeButtons.anchoredPosition;
            pos.y += delta.y * speed;
            if ((delta.y < 0 && pos.y >= 0) || (UIHelpers.RectOverlaps(scrollThemeControll, buttons) == false && delta.y > 0))
            {
                themeButtons.anchoredPosition = pos;
            }
        }
        else if (blockSwitchController.activeBlock == 5)
        {
            Vector2 pos = messeGess.anchoredPosition;
            pos.y += delta.y * speed;
            if (fulMesButton.activeSelf == false && dataStorage.messages.Count > 0)
            {
                if ((delta.y < 0 && pos.y >= 0) || (delta.y > 0 && !UIHelpers.RectOverlaps(Checker327, messeGess.transform.GetChild(messeGess.childCount - 1).GetComponent<RectTransform>()) && dataStorage.messages.Count >=4))
                {
                        messeGess.anchoredPosition = pos;
                    
                }

            }
            else if (dataStorage.messages.Count == 0)
            {
                messeGess.anchoredPosition = new Vector2(0, 0);
            }
        }

    }
}
public static class UIHelpers
{
    public static bool RectOverlaps(RectTransform firs, RectTransform second)
    {
        Rect rectA = GetWorldRect(firs);
        Rect rectB = GetWorldRect(second);

        return rectA.Overlaps(rectB);
    }
    public static Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        return new Rect(
            corners[0].x,
            corners[0].y,
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y
        );
    }
}
