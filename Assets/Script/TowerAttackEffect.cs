using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  반드시 컴포넌트를 필요로 할 때!
[RequireComponent(typeof(Tower))]
public class TowerAttackEffect : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;
    public float bulletSpeed;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (!TryGetComponent<Tower>(out Tower tower))
        {
            //  컴포넌트가 존재하는지 확인하는 구문 + 프로그램 종료 + 문자열이 로그로 뜬다.
            throw new MissingComponentException("Tower 컴포넌트가 존재하지 않습니다.");
        }
        tower.onAttack.AddListener(Fire);
        
        //  초기화
        firePos = transform;
    }

    private void Fire(Monster target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletEffect>().SetTarget(target.transform.position, bulletSpeed);
        
        _audioSource.Play();
    }
}
