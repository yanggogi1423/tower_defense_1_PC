using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float limitTime;
    public TextMeshProUGUI text;

    public UnityEvent onPhaseChange = new UnityEvent();
    public UnityEvent onGameStart = new UnityEvent();
    public UnityEvent onMonsterSpawnStart = new UnityEvent();
    public UnityEvent onLastPhase = new UnityEvent();

    private int phase;

    private void Start()
    {
        phase = -1;

        onGameStart.Invoke();
    }

    private void FixedUpdate()
    {
        limitTime -= Time.deltaTime;

        var currentTime = Math.Round(limitTime);

        text.SetText("Time " + currentTime.ToString());

        //  페이즈 설정

        if (limitTime <= 245.0 && phase == -1)
        {
            phase++;
            onMonsterSpawnStart.Invoke();
        }

        if (phase == 0 && limitTime <= 210.0)
        {
            phase++;
            Debug.Log("phase Up");
            onPhaseChange.Invoke();
        }
        else if (phase == 1 && limitTime <= 160.0)
        {
            phase++;
            Debug.Log("phase Up");
            onPhaseChange.Invoke();
        }
        else if (phase == 2 && limitTime <= 110.0)
        {
            phase++;
            Debug.Log("phase Up");
            onPhaseChange.Invoke();
        }
        else if (phase == 3 && limitTime <= 75.0)
        {
            phase++;
            Debug.Log("Last Phase");
            onPhaseChange.Invoke();
            onLastPhase.Invoke();
        }

        if (limitTime <= 0.0)
        {
            Debug.Log("Game Clear");

            GameManager.GetInstance().GetComponent<ChangeScene>().GameClear();
        }
    }
}