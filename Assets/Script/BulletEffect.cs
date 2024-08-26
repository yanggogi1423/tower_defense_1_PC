using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
   private Vector3 target;

   public void SetTarget(Vector3 targetPos, float speed)
   {
      target = targetPos;
      GetComponent<Rigidbody2D>().velocity = (target - transform.position).normalized * speed;
   }

   private void Update()
   {
      // Mathf는 유니티에서 제공하는 함수, Math.Epsilon은 최솟값으로 지정된 임의의 숫자(충돌 관련에서 사용)
      // if (Vector2.Distance(transform.position, target) < Mathf.Epsilon) -> 근데 너무 작아서 작동하지 않을 수 있다!
      if (Vector2.Distance(transform.position, target) < 0.1)
      {
         Destroy(gameObject);
      }
   }
}
