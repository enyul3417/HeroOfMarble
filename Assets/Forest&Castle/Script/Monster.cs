using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public string enemyname; // 적 이름
    public int maxHp; // 최대 체력
    public int nowHp; // 현재 체력
    public int atkDmg = 0; // 공격력
    public int atkSpeed; // 공격속도
    
    protected void SetEnemyStatus(string _enemyname, int _maxHp, int _atkDmg, int _atkSpeed, float _walkForce)
    {
        enemyname = _enemyname;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        walkForce = _walkForce;
    }

    public PlayerController player;

    public GameObject hpPrefab;
    protected GameObject hpBar;
    public GameObject canvas;
    public float height = 1.0f; // 체력바가 몬스터 위에 뜨는 높이

    protected Animator animator;
    Vector3 movement;
    int movementFlag = 0; // 0: Idle, 1: Left, 2: Right
    public float walkForce;
    bool isTracing;
    GameObject traceTarget;
    bool isDie = false;



    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(hpPrefab, canvas.transform);

        animator = gameObject.GetComponentInChildren<Animator>();

        // 몬스터 이름에 따라 스탯 정하기
        // 스탯은 변경하셔도 됩니다,(테스트하기 편하게 잡아뒀어요)
        if (name.Equals("Goblin"))
        {
            SetEnemyStatus("Goblin", 10, 2, 1, 1.5f);
        }
        if (name.Equals("FlyingEye"))
        {
            SetEnemyStatus("FlyingEye", 20, 3, 2, 1.5f);
        }
        if (name.Equals("Mushroom"))
        {
            SetEnemyStatus("Mushroom", 30, 5, 1, 1.0f);
        }
        if (name.Equals("Skeleton1"))
        {
            SetEnemyStatus("Skeleton1", 15, 2, 1, 1.5f);
        }
        if(name.Equals("Slime"))
        {
            SetEnemyStatus("Slime", 40, 4, 1, 1.0f);
        }

        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        HPBar();
        Move();
        Attack();
    }

    // 인상적이라 생각하는 코드
    protected void HPBar()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0)); 
        if (!isDie) // 몬스터가 살아있다면
        {
            hpBar.transform.position = _hpBarPos; // 체력바가 몬스터 위를 따라다님
            this.hpBar.GetComponent<Image>().fillAmount = (float)nowHp / (float)maxHp;
        }
    }

    // 인상적이라 생각하는 코드
    protected void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing) // 플레이어 쫓아가기
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else // 플레이어가 범위 안에 없으면 랜덤하게 돌아다니기
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        // 몬스터가 움직이는 방향에 따라 좌우 바꿔주기
        if(dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1 * this.transform.localScale.y, this.transform.localScale.y, 1);

        }
        else if(dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(this.transform.localScale.y, this.transform.localScale.y, 1);

        }
        transform.position += moveVelocity * walkForce * Time.deltaTime;
    }

    protected void Attack()
    {
        Vector2 p1 = transform.position;
        Vector2 p2 = this.player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;

        // 플레이어와 몬스터가 일정 거리 안에 있다면
        if (d < 5.0f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            this.animator.SetTrigger("AttackTrigger");
    }

    public void Die()
    {
        // 움직임 멈추기
        StopCoroutine("ChangeMovement");
        isDie = true;
        animator.SetBool("isDie", isDie);
        SoundManager.instance.PlaySE("몬스터 dead"); //몬스터 죽었을때 효과음

        // 몬스터와 체력바 제거
        Destroy(gameObject, 1f);
        Destroy(hpBar.gameObject);

    }


    // Coroutine
    protected IEnumerator ChangeMovement()
    {
        // 0~3까지 movementFlag 랜덤
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        // 3초 대기
        yield return new WaitForSeconds(3f);

        // 반복
        StartCoroutine("ChangeMovement");
    }

    public void Damaged()
    {
        nowHp -= player.getAtkDamage();
        animator.SetBool("isDamaged", true);
        Debug.Log(nowHp);
        animator.SetBool("isDamaged", false);
        if (nowHp <= 0) // 적 사망
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 주변에 플레이어가 있다면
        {
            traceTarget = collision.gameObject; // 따라다닐 목표 설정

            StopCoroutine("ChangeMovement"); // 무작위 이동은 멈추기
        }


        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // 플레이어가 공격 동작을 하는 중이고
        {
            if (collision.gameObject.tag == "Weapon") // 몬스터가 무기에 닿았다면
                Damaged(); // 데이미지 입기
        }

        if (collision.gameObject.tag == "Player") // 플레이어와 닿으면
        {
            player.Damaged(atkDmg); // 플레이어 체력 감소
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 주변에 플레이어가 있다면
        {
            isTracing = true; // 따라다니는 상태가 됨
            animator.SetBool("isMoving", true); // 움직이는 애니메이션
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 플레이어와 떨어지게되면
        {
            isTracing = false; // 안따라다님

            StartCoroutine("ChangeMovement"); // 무작위로 돌아다니기
        }
    }
}
