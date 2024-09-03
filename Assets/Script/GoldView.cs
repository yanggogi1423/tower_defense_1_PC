using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GoldView : MonoBehaviour
{
    private void Awake()
    {
        GameManager.GetInstance().onGoldChanged.AddListener(UpdateGold);
        UpdateGold();
    }

    private void UpdateGold()
    {
        string gold = GameManager.GetInstance().GetGold().ToString();

        //  SetText 함수를 이용
        GetComponent<TextMeshProUGUI>().SetText("Gold " + gold);
    }
}
