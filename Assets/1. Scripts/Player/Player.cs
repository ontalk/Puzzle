using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Instance
    {
        get { return _instance; }
    }
    [NonSerialized]
    public Vector3 MoveTo; // 캐릭터 이동 방향을 나타내는 벡터
    private float Horizontal_Move; // 수평 입력 (좌우)
    private float Vertical_Move; // 수직 입력 (전후)

    #region 값 관리
    public float JumpPulse = 10; // 점프력
    private float currentMoveSpeed; // 현재 이동 속도
    [SerializeField] private float initialMoveSpeed = 5f; // 초기 이동 속도
    [SerializeField] private float maxMoveSpeed = 13f; // 최대 이동 속도
    #endregion
    #region 키 관리
    private bool JumpDown;
    #endregion
    #region 상태 관리
    private bool isMove = false;
    private bool isJump = false;
    private bool triggerBox; // 만약 안보이는 공간에 부딫히게 된다면 장애물 실행.
    private bool isDamage = false;
    [NonSerialized] public bool isDead = false;
    #endregion
    #region Inspector 관리
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public AudioClip DieClip;//오디오
    public AudioClip JumpClip;//오디오
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        DirectionMove();
        Jump();
        Landing();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void GetInput() 
    {
        JumpDown = Input.GetButtonDown("Jump");
    }

    private void DirectionMove()
    {
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1f;
    }
    private void Move()
    {

        Horizontal_Move = Input.GetAxisRaw("Horizontal"); // 수평 입력 (좌우)
        rigid.AddForce(Vector2.right * Horizontal_Move, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxMoveSpeed) //오른쪽
            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxMoveSpeed * (-1)) //왼쪽
            rigid.velocity = new Vector2(maxMoveSpeed * (-1), rigid.velocity.y);
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);
        isMove = true;
        
            
    }
    void Jump()
    {
        if (JumpDown && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector3.up * JumpPulse, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            SoundManager.instance.SfXPlay("Jump", JumpClip);
        }
    }

    void Landing()
    {
        if (rigid.velocity.y < 0)
        {

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));//레이어 이름에 해당하는 정수값을 리턴

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.4f)
                    anim.SetBool("isJump", false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            isDead = true;
            Die();
            
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        transform.position = spawnPoint.transform.position;
        isDead = false;
        //SoundManager.instance.SfXPlay("Head", DieClip);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //씬 다시 로드
    }

}
