using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
   private Vector3 target;
   public float moveSpeed;
   
   public void SetTarget(Vector3 targetPos, float speed)
   {
      target = targetPos;

      StartCoroutine(MoveRoutine());
   }

   private IEnumerator MoveRoutine()
   {
      float dist = Vector3.Distance(transform.position, target);
      float duration = dist / moveSpeed;

      float t = 0;
      while (true)
      {
         t += Time.deltaTime;
         if (t >= duration)
         {
            Destroy(gameObject);
            yield break;
         }

         float percentage = t / duration;
         transform.position = Vector3.Lerp(transform.position, target, percentage);

         yield return null;
      }
   }
}
