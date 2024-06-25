using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedButtons : MonoBehaviour
{
    public WantedLevel wantedLevel;
    public void WantedLevel1()
    {
        wantedLevel.maskValue = 61;
    }

    public void WantedLevel2()
    {
        wantedLevel.maskValue = 119;
    }
    public void WantedLevel3()
    {
        wantedLevel.maskValue = 179;
    }
    public void WantedLevel4()
    {
        wantedLevel.maskValue = 238;
    }
    public void WantedLevel5()
    {
        wantedLevel.maskValue = 297.9f;
    }
}
