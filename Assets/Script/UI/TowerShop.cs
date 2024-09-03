using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    private Coroutine uiMoveRoutine;

    [SerializeField] private float moveDuration = 0.5f;

    [SerializeField] private AnimationCurve moveCurve;

    private float _showY;
    private float _hideY;

    [SerializeField] private Button[] _buttons;

    private void Awake()
    {
        GameManager.GetInstance().onTowerSelected.AddListener(OnTowerSelected);

        _showY = GetComponent<RectTransform>().anchoredPosition.y;
        _hideY = _showY - GetComponent<RectTransform>().rect.height;

        GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                GetComponent<RectTransform>().anchoredPosition.x,
                _hideY);
    }

    private void Update()
    {
        foreach (var but in _buttons)
        {
            but.interactable = true;
        }
        if (GameManager.GetInstance().GetTower() != null)
        {
            if (GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().isFull)
            {
                foreach (var but in _buttons)
                {
                    but.interactable = false;
                }
            }
        }
    }

    private void OnTowerSelected()
    {
        if (uiMoveRoutine != null) StopCoroutine(uiMoveRoutine);

        if (SelectedAnyTower())
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }

    private static bool SelectedAnyTower()
    {
        return GameManager.GetInstance().GetTower() != null;
    }

    private void ShowUI()
    {
        uiMoveRoutine = StartCoroutine(MoveRoutine(true));
    }

    private void HideUI()
    {
        uiMoveRoutine = StartCoroutine(MoveRoutine(false));
    }

    private IEnumerator MoveRoutine(bool show)
    {
        //  Reference로 받아온다.
        RectTransform tsf = GetComponent<RectTransform>();

        Vector2 startPos = tsf.anchoredPosition;

        //  종결 위치 show를 통해 판단
        Vector2 endPos = new(tsf.anchoredPosition.x, show ? _showY : _hideY);

        float time = 0;
        while (time < moveDuration)
        {
            time += Time.deltaTime;

            var ev = moveCurve.Evaluate(time / moveDuration);

            tsf.anchoredPosition = Vector2.Lerp(startPos, endPos, ev);
            yield return null;
        }

        tsf.anchoredPosition = endPos;
    }
}