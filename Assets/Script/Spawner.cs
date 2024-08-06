using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterOriginal;
    public float spawnRate;

    public Transform[] path;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    //  코루틴(Coroutine); MFC의 Timer와 같은 기능이라고 생각하면 편하다.
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            //  Instatiate는 새로운 게임 오브젝트를 설정한 인자에 따라 생성할 수 있다.
            //  Instantiate(prefab, position, rotation)
            //  prefab은 게임 오브젝트를 효율적으로 관리하고 재사용하기 위한 기능.
            GameObject obj = Instantiate(monsterOriginal, Vector3.zero, Quaternion.identity);
            
            //  GetComponent는 game object에 붙어있는 특정 컴포넌트를 가져온다. -> <>안에 component의 이름을 작성한다.
            Monster monsterComponent = obj.GetComponent<Monster>();
            
            monsterComponent.SetPath(path);
            
            //  List에 추가
            GameManager.GetInstance().AddMonster(monsterComponent);
            
            //  쿨타임아라고 생각하자. return : 반복 주기 설정 가능, break : 함수 종료
            yield return new WaitForSeconds(spawnRate);
        }
    }
}