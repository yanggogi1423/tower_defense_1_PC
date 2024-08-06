using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    public int maxIndex;
    public Transform[] paths;

    public float maxHp;

    private int curIndex = 0;
    private Vector3 direction = Vector3.zero;
    [SerializeField] private float hp;

    //  paths는 함수를 통해 외부에서 설정한다.
    private void Awake()
    {
        hp = maxHp;
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

    public void SetPath(Transform[] path)
    {
        paths = path;
        maxIndex = path.Length;
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //  여기에서는 gameObject가 아닌 this 사용한다. -> instance를 가져와서 삭제하는 것이기 때문
        //  !Important - C#에서는 this가 instance를 참조하는 용도로 사용. 포인터가 아님
        //  싱글톤일 때만 권장되는 문법이다. GameManager의 타입의 instance를 가져오고 그 내부의 public함수를 사용할 수 있음.
        GameManager.GetInstance().RemoveMonster(this);
        //  그 후에 이 instance를 삭제
        Destroy(gameObject);
    }
}
