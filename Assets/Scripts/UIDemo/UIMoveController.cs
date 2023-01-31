using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMoveController : MonoBehaviour
{
    public Image[] imageWADS;
    public Image[] arrow;
    public Image space;

    public void PressWADS(int index)
    {
        imageWADS[index].color = new Color32(255, 0, 255, 255);
        arrow[index].color = new Color32(255, 0, 255, 255);
    }

    public void UnPressWADS(int index)
    {
        imageWADS[index].color = new Color32(255, 255, 255, 255);
        arrow[index].color = new Color32(255, 255, 255, 255);
    }
    public void PressSpace()
    {
        space.color = new Color32(255, 0, 255, 255);
    }
    public void UnPressSpace()
    {
        space.color = new Color32(255, 255, 255, 255);
    }

}
