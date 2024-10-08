using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            //  존재하는 지 확인한다.
            _instance = FindObjectOfType<GameManager>();
            if (_instance != null) return _instance;

            //  존재하지 않는다면 생성한다.
            _instance = new GameManager().AddComponent<GameManager>();
            _instance.name = "GameManager";
        }

        return _instance;
    }

    private void Awake()
    {
        //  존재하지 않는다면 this를 GM으로
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            //  존재하지만 this가 아니라면 destroy
            Destroy(gameObject);
        }
    }

    #endregion

    //  Max 체력
    private const int MAX_HP = 3;
    [SerializeField, ReadOnly(true)] private int hp = MAX_HP;

    //  생성되는 몬스터 객체의 List
    [SerializeField] private List<Monster> _monsters = new();

    [SerializeField] private int gold;

    public UnityEvent onHpChanged = new UnityEvent();
    public UnityEvent onGoldChanged = new UnityEvent();

    //  Tower

    public GameObject _clickedTower;
    public UnityEvent onTowerSelected = new UnityEvent();

    private void Start()
    {
        //  게임 시작 시 체력을 초기화
        hp = MAX_HP;
        onHpChanged.Invoke();

        _clickedTower = null;
    }

    public void GetDamage()
    {
        hp--;

        //  양이 아닌 정수일 때 GAME OVER, hp를 0으로 초기화
        if (hp <= 0)
        {
            hp = 0;
            Debug.Log("Game Over");
            
            gameObject.GetComponent<ChangeScene>().GameOver();
        }

        onHpChanged.Invoke();
    }

    public int GetHp()
    {
        return hp;
    }

    //  필드에 존재하는 모든 몬스터를 가져온다.(List를 array로 변환)
    public Monster[] GetAllMonster()
    {
        return _monsters.ToArray();
    }

    public void AddMonster(Monster m)
    {
        _monsters.Add(m);
    }

    public void RemoveMonster(Monster m)
    {
        //  List에 iterator가 아닌 instance를 넣어도 삭제할 수 있다.
        _monsters.Remove(m);
    }

    //  Gold

    public void AddGold(int value)
    {
        gold += value;
        onGoldChanged.Invoke();
    }

    public int GetGold()
    {
        return gold;
    }

    public void UseGold(int value)
    {
        if (gold < value)
        {
            Debug.Log("Not Enough Gold");
            return;
        }

        gold -= value;
        onGoldChanged.Invoke();
    }

    public GameObject GetTower()
    {
        return _clickedTower;
    }

    public void SetTower(GameObject tower)
    {
        _clickedTower = tower;
        onTowerSelected.Invoke();
    }

    public void Update()
    {
        //  우클릭 시 clickedTower reset
        if (Input.GetMouseButtonDown(1))
        {
            if (_clickedTower != null)
            {
                _clickedTower = null;
                onTowerSelected.Invoke();
            }
        }

        // if (GetTower() != null)
        // {
        //     if (GetTower().GetComponent<TowerPoint>().isFull)
        //     {
        //         _clickedTower = null;
        //         onTowerSelected.Invoke();
        //     }
        // }
    }
}