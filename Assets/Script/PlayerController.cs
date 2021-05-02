using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    public static object MyInstance { get; internal set; } 
    
    Rigidbody2D rigidbody2D;
    public Animator animator;
    public GameObject start; // 시작 포탈

    private int maxHp; // 최대 체력
    private int nowHp; // 현재 체력
    private int atkDmg = 0; // 공격력
    private float atkSpeed = 1; // 공격 속도
    bool attacked = false; // 공격했는가

    GameObject hpBar; // 체력바

    public float jumpForce = 350.0f; // 점프할때 받는 힘
    public float walkForce = 15.0f; // 걸어가는 힘
    public float maxWalkSpeed = 2.0f; // 최대 속력
    bool isGrounded = false; // 땅에 닿았는가
    bool isDamaged = false; // 데미지를 입었는가

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        nowHp = 100;
        atkDmg = 10;
        atkSpeed = 1.5f;

        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.hpBar = GameObject.Find("player_hp_bar");
        

        this.transform.position = start.transform.position; // 시작 포탈 위치에서 플레이어가 게임 시작
    }

    // Update is called once per frame
    void Update()
    {
        this.hpBar.GetComponent<Image>().fillAmount = (float)nowHp / (float)maxHp; // 남은 체력 표시
        Move();
        Attack();
        Die();
    }
    

    // 캐릭터 이동
    protected void Move()
    {
       
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // 스페이스바를 눌렀고, 플레이어가 땅에 닿은 상태라면
        {
            // 점프 애니메이션
            this.animator.SetTrigger("JumpTrigger");
            isGrounded = false; // 다중 점프 방지
            // 위쪽 방향으로 힘을 가함. 중력에 의해 플레이어는 내려옴
            this.rigidbody2D.AddForce(transform.up * this.jumpForce);
        }   

        // 좌우 이동
        int key = 0;
        if (Input.GetKey(KeyCode.A)) key = -1; // 왼쪽 이동
        if (Input.GetKey(KeyCode.D)) key = 1; // 오른쪽 이동

        // 플레이어 속도
        float moveSpeed = Mathf.Abs(this.rigidbody2D.velocity.x);

        // 스피트 제한
        if (moveSpeed < this.maxWalkSpeed)
        {
            this.rigidbody2D.AddForce(transform.right * key * this.walkForce); // key 값에 따라 좌우 이동 변화
        }

        // 움직이는 방향에 따라 캐릭터 방향 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(-key * this.transform.localScale.y, this.transform.localScale.y, 1);
        }

        // 좌우 입력되면 달리는 애니메이션으로 변경
        animator.SetFloat("Speed", Mathf.Abs(key));
    }

    protected void Attack()
    {
        // 좌클릭을 했고, Attack 애니메이션이 실행되지 않고 있는 상태라면 공격
        if (Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) 
        {
            // 공격 애니메이션
            this.animator.SetTrigger("AttackTrigger");
            SoundManager.instance.PlaySE("NormalGun_1"); //공격시 효과음 추가
         
        }
    }
    
    // 아래 2개의 코드는 애니메이션에서 공격 상태를 결정하는 용도
    void AttackTrue()
    {
        attacked = true;
    }

    void AttackFalse()
    {
        attacked = false;
    }

    public int getAtkDamage()
    {
        return atkDmg;
    }


    // 트리거가 두번 돌아서 구슬 조각이 2개가 되는 문제가 있음
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Marble") // 구슬과 닿으면
        {
            Destroy(collision.gameObject, 0f); // 구슬 없애기
            MarbleManager.setCount(1); // 구슬 획득 값 +1
            SoundManager.instance.PlaySE("크리스탈"); //구슬 얻을 때 효과음 추가
        }
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground") // 땅에 닿았을때만 점프 가능
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
        }
    }

    IEnumerator wait()
    {
        // 3초 대기
        yield return new WaitForSeconds(3f);
    }

    public void Damaged(int dmg)
    {
        nowHp -= dmg;
        Debug.Log($"Player HP: {nowHp}");  
    }

    void Die()
    {
        if(nowHp < 0)
        {
            animator.SetBool("isDie", true);
            wait();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
            SoundManager.instance.PlaySE("Game over"); //Game over 효과음 추가
            SoundManager.instance.PlaySE("용사 dead"); //공격받아 죽을때 효과음 추가
        }
    }

  
}

