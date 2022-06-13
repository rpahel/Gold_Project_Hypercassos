using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    public Sprite spriteOff;
    public Sprite spriteOn;
    public SpriteRenderer spriteRenderer;
    public GameObject playerSwitch;
    public GameObject boxSwitch;

    void Update()
    {
        if (playerSwitch != null && playerSwitch.GetComponent<SwitchPressurePlayer>().isActive == true)
                spriteRenderer.sprite = spriteOn;
        if (boxSwitch != null && boxSwitch.GetComponent<SwitchPressureBox>().isActive == true)
            spriteRenderer.sprite = spriteOn;
        if (boxSwitch != null && boxSwitch.GetComponent<SwitchPressureBox>().isActive == false)
            spriteRenderer.sprite = spriteOff;
    }
}
