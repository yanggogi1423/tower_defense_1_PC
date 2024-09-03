using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TowerSpawner : MonoBehaviour
{
    public GameObject[] towerPrefab;
    public Transform[] spawnPos;

//  각각 타워의 금액을 다르게 하기 위해서 towerPrefab에서 가져옴
//    [SerializeField] private int cost;

//    private int idx;

    //  타워 판매를 위한 Event
    public UnityEvent onSellTower = new UnityEvent();
    public UnityEvent onCantSellTower = new UnityEvent();

    public UnityEvent onNotEnoughMoney = new UnityEvent();
    public UnityEvent onCantBuildThere = new UnityEvent();

    //  z 파이팅
    private Vector3 _zFighting = new Vector3(0, 0, -0.5f);

    private void Awake()
    {
        GameManager.GetInstance().onTowerSelected.AddListener(OnTowerSelected);
    }

    private void OnTowerSelected()
    {
        DefaultColor();
    }

    //  Tower List
    public void SpawnDefaultTower()
    {
        if (CheckingArea() && EnoughGold(0))
        {
            GameManager.GetInstance().UseGold(towerPrefab[0].GetComponent<Tower>().cost);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().tower =
                Instantiate(towerPrefab[0],
                    GameManager.GetInstance().GetTower().transform.position + _zFighting,
                    Quaternion.identity);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().isFull = true;

            //  선택 해제
            GameManager.GetInstance().SetTower(null);
        }
        else if (!EnoughGold(0))
        {
            onNotEnoughMoney.Invoke();
        }
        else
        {
            //  아마 뜨지는 않을 거임(이미 막아둠) -> 혹시 모를 오류에 대비한 코드
            onCantBuildThere.Invoke();
        }
    }

    public void SpawnRapidTower()
    {
        if (CheckingArea() && EnoughGold(1))
        {
            GameManager.GetInstance().UseGold(towerPrefab[1].GetComponent<Tower>().cost);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().tower =
                Instantiate(towerPrefab[1],
                    GameManager.GetInstance().GetTower().transform.position + _zFighting,
                    Quaternion.identity);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().isFull = true;

            //  선택 해제
            GameManager.GetInstance().SetTower(null);
        }
        else if (!EnoughGold(1))
        {
            onNotEnoughMoney.Invoke();
        }
        else
        {
            onCantBuildThere.Invoke();
        }
    }

    public void SpawnOnagerTower()
    {
        if (CheckingArea() && EnoughGold(2))
        {
            GameManager.GetInstance().UseGold(towerPrefab[2].GetComponent<Tower>().cost);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().tower =
                Instantiate(towerPrefab[2],
                    GameManager.GetInstance().GetTower().transform.position + _zFighting,
                    Quaternion.identity);

            GameManager.GetInstance().GetTower().GetComponent<TowerPoint>().isFull = true;

            //  선택 해제
            GameManager.GetInstance().SetTower(null);
        }
        else if (!EnoughGold(2))
        {
            onNotEnoughMoney.Invoke();
        }
        else
        {
            onCantBuildThere.Invoke();
        }
    }

    //  Sell Tower
    public void SellTheTower()
    {
        if (CheckingArea())
        {
            TowerPoint tmp = GameManager.GetInstance().GetTower().GetComponent<TowerPoint>();
            if (tmp.tower != null)
            {
                GameManager.GetInstance().AddGold(tmp.tower.GetComponent<Tower>().cost * 8 / 10);
                tmp.isFull = false;

                Debug.Log("Sell Succeed");
                onSellTower.Invoke();
                Destroy(tmp.tower);
            }

            else // Unable to sell
            {
                onCantSellTower.Invoke();
                Debug.Log("Unable to sell");
            }
        }
    }

    private bool EnoughGold(int i)
    {
        return GameManager.GetInstance().GetGold() >= towerPrefab[i].GetComponent<Tower>().cost;
    }

    private bool CheckingArea()
    {
        return (GameManager.GetInstance().GetTower() != null);
    }

    public void DefaultColor()
    {
        for (int i = 0; i < spawnPos.Length; i++)
        {
            Color color = Color.white;
            color.a = 0.5f;
            spawnPos[i].GetComponent<SpriteRenderer>().color = color;
        }
    }
}