using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour
{
    public GameObject subBox;
    public TextMeshProUGUI subInText;
    public Animator subAni;

    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);

    private void Start()
    {
        subBox.SetActive(false);
    }

    public void Show(String message)
    {
        subInText.SetText(message);
        subBox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(SubDelay());
    }

    private IEnumerator SubDelay()
    {
        subBox.SetActive(true);
        subAni.SetBool("show",true);
        yield return _UIDelay1;
        
        subAni.SetBool("show",false);
        yield return _UIDelay2;
        subBox.SetActive(false);
    }
}
