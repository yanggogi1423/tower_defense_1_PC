using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    public GameObject[] hpImages;

    public void UpdateHp()
    {
        int hp = GameManager.GetInstance().GetHp();

        for (int i = 0; i < hpImages.Length; i++)
        {
            if (i < hp)
            {
                hpImages[i].SetActive(true);
            }
            else
            {
                hpImages[i].SetActive(false);
            }
        }
    }
    
}
