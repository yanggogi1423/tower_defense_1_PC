using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject[] towerPrefab;
    public Transform[] spawnPos;

//  각각 타워의 금액을 다르게 하기 위해서 towerPrefab에서 가져옴
//    [SerializeField] private int cost;

//    private int idx;

    private void Start()
    {
//        idx = 0;
    }

    public void SpawnDefaultTower()
    {
        if (CheckingArea()&&EnoughGold(0))
        {
            GameManager.GetInstance().UseGold(towerPrefab[0].GetComponent<Tower>().cost);
            Instantiate(towerPrefab[0], GameManager.GetInstance().GetTower().transform.position, Quaternion.identity);
        }
    }
    
    public void SpawnRapidTower()
    {
        if (CheckingArea()&&EnoughGold(1))
        {
            GameManager.GetInstance().UseGold(towerPrefab[1].GetComponent<Tower>().cost);
            Instantiate(towerPrefab[1], GameManager.GetInstance().GetTower().transform.position, Quaternion.identity);
        }
    }

    private bool EnoughGold(int i)
    {
        return GameManager.GetInstance().GetGold() >= towerPrefab[i].GetComponent<Tower>().cost;
    }

    private bool CheckingArea()
    {
        return GameManager.GetInstance().GetTower() != null;
    }

    public void DefaultColor()
    {
        for (int i = 0; i < spawnPos.Length; i++)
        {
            spawnPos[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
