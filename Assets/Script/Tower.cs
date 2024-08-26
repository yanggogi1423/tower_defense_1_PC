using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    public float damage;
    public float atkCooldown;
    public float range;
    
    //  Tower Cost
    public int cost;

    public UnityEvent<Monster> onAttack = new UnityEvent<Monster>();

    public void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void Attack(Monster target)
    {
        if (target == null) return;

        target.TakeDamage(damage);
        onAttack.Invoke(target);
    }

    public Monster DetectTarget()
    {
        // 가장 가까운 target을 공격하도록 코드를 작성해보자.

        Monster[] monsters = GameManager.GetInstance().GetAllMonster();

        Monster target = null;
        float minDist = float.MaxValue;

        foreach (Monster m in monsters)
        {
            //  null인 경우 skip
            if (m == null) continue;
            float dist = Distance(m.transform);

            if (dist < minDist)
            {
                target = m;
                minDist = dist;
            }
        }

        if (minDist > range) return null;

        return target;
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Monster target = DetectTarget();
            //  null일 때는 실행할 필요가 없기에 구현해 봄.
            if (target != null)
            {
                Attack(target);
                Debug.Log("Atk");
            }
            yield return new WaitForSeconds(atkCooldown);
        }
    }

    private float Distance(Transform other)
    {
        Vector3 a = transform.position;
        //  transform 자체를 들고 옴
        Vector3 b = other.position;

        return Vector3.Distance(a, b);
    }

}
