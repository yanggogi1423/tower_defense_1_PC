using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{

    [SerializeField] private GameObject towerSpawner;

    public GameObject tower;

    public bool isFull;

    private void Start()
    {
        isFull = false;
        tower = null;
    }

    private void OnMouseDown()
    {
        if (!isFull)
        {
            GameManager.GetInstance().SetTower(gameObject);
            towerSpawner.GetComponent<TowerSpawner>().DefaultColor();

            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else 
        {
            GameManager.GetInstance().SetTower(gameObject);
        }
    }

    public void DeleteTower()
    {
        Destroy(tower);
    }
}
