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
      if (Vector2.Distance(transform.position, target) < 0.1f)
      {
         Destroy(gameObject);
      }
   }
}
