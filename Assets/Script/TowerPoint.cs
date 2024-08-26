using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{

    [SerializeField] private GameObject towerSpawner;

    private bool isFull;

    private void Start()
    {
        isFull = false;
    }

    private void OnMouseDown()
    {
        if (!isFull)
        {
            GameManager.GetInstance().clickedTower = gameObject;
            towerSpawner.GetComponent<TowerSpawner>().DefaultColor();

            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
