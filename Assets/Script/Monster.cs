using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Transform[] paths;
    private int curIndex = 0;
    public int maxIndex;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        maxIndex = paths.Length;
    }

    public void FixedUpdate()
    {
        if (maxIndex - 1 < curIndex)
        {
            //  종료 조건
            //  그러나 이렇게 되면 segementation fault(IndexOutOfRangeExpectation)가 발생
            //  지속적으로 return을 하는 것보다는 Component 자체를 삭제하거나, 비활성화 하는 방향으로
            //  Destroy(gameObject)
            GameManager.GetInstance().GetDamage();
            Destroy(gameObject);
            return;
        }

        CheckNextPath();

        //  Lerp는 선형 보간이다. -> 움직임을 부드럽게
        transform.position = Vector3.Lerp(transform.position,
        transform.position + direction,
        moveSpeed * Time.deltaTime);
    }

    private void CheckNextPath()
    {
        if (maxIndex - 1 < curIndex)
        {
            return;
        }

        if (paths[curIndex].position.magnitude - transform.position.magnitude < 0.00001)
        {
            curIndex++;
            if (curIndex == maxIndex)
            {
                return;
            }
            direction = paths[curIndex].position - paths[curIndex - 1].position;
        }
    }



}

/* 재오 사수님 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Transform[] paths;
    private int curIndex = 0;
    public int maxIndex;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        maxIndex = paths.Length;
    }

    public void FixedUpdate()
    {
        if (maxIndex - 1 < curIndex)
        {  
            //  종료 조건
            Destroy(this);
            return;
        }

        CheckNextPath();
        
        //  Lerp는 선형 보간이다. -> 움직임을 부드럽게
        transform.position = Vector3.Lerp(transform.position, 
        transform.position + direction, 
        moveSpeed * Time.deltaTime);
    }

    private void CheckNextPath()
    {
        if (maxIndex - 1 < curIndex)
        {
            return;
        }

        if (paths[curIndex].position.magnitude - transform.position.magnitude < 0.00001)
        {
            curIndex++;
            if (curIndex == maxIndex)
            {
                return;
            }
            direction = paths[curIndex].position - paths[curIndex - 1].position;
        }
    }
}
 */
