using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public int block;
    public void OnClicked()
    {
        FindAnyObjectByType<BlockSwitchController>().InizializateBlock(block);
    }
}
