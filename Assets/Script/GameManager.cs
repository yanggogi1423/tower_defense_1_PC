using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
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

    private const int MAX_HP = 3;
    [SerializeField, ReadOnly(true)] private int hp = MAX_HP;

    public UnityEvent onHpChanged = new UnityEvent();

    public void GetDamage()
    {
        hp--;
        
        //  양이 아닌 정수일 때 GAME OVER, hp를 0으로 초기화
        if (hp <= 0)
        {
            hp = 0;
            Debug.Log("Game Over");
        }

        onHpChanged.Invoke();
    }

    public int GetHp()
    {
        return hp;
    }
}