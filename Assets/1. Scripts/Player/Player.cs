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
    public float duration = 0.5f;
    private float startTime;
    #endregion
    #region 키 관리
    private bool JumpDown;
    #endregion
    #region 상태 관리
    private bool isMove = false;
    bool isJump = false;
    public bool canDoubleJump = false;
    public bool isScale = false;
    private bool triggerBox; // 만약 안보이는 공간에 부딫히게 된다면 장애물 실행.
    private bool isDamage = false;
    public bool isInversion = false;
    [NonSerialized] public bool isDead = false;
    [NonSerialized] public bool isHorizontalInversion = false;
    #endregion
    #region Inspector 관리
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public GameObject Boost;
    public AudioClip DieClip;//오디오
    public AudioClip JumpClip;//오디오
    private Vector3 originalScale;
    private Vector3 targetScale;
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
    }
    

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        FScale();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        DirectionMove();
        ActualJump(); 
        //Jump();
        Landing();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region 움직임
    private void DirectionMove()
    {
        #region 중력반전 X
       /* if (!isInversion && !isHorizontalInversion)
        {
            if (Input.GetButtonDown("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1f;
        }
        else
        {
            if (Input.GetButtonDown("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1f;
        }

        if (!isInversion && isHorizontalInversion)
        {
            if (Input.GetButtonDown("InversionHorizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("InversionHorizontal") == 1f;
        }
        else
        {
            if (Input.GetButtonDown("InversionHorizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("InversionHorizontal") == -1f;
        }*/
        #endregion
        #region 중력반전 O
        if (isInversion && !isHorizontalInversion)
        {
            if (Input.GetButtonDown("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1f;
        }
        else
        {
            if (Input.GetButtonDown("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1f;
        }

        if (isInversion && isHorizontalInversion)
        {
            if (Input.GetButtonDown("InversionHorizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("InversionHorizontal") == -1f;
        }
        else
        {
            if (Input.GetButtonDown("InversionHorizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("InversionHorizontal") == 1f;
        }
        #endregion

    }
    private void Move()
    {
        if (!isHorizontalInversion)
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
        else if(isHorizontalInversion)
        {
            Horizontal_Move = Input.GetAxisRaw("InversionHorizontal"); // 수평 입력 (좌우)
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
    }

    #endregion

    #region 점프
    private void GetInput()
    {
        JumpDown = Input.GetButtonDown("Jump");
    }
    void ActualJump()
    {
        if (JumpDown && !anim.GetBool("isJump") && !isInversion)
        {
            Jump();
        }
        else if (JumpDown && anim.GetBool("isJump") && canDoubleJump && !isInversion)
        {
            canDoubleJump = false;
            Jump();
        }else if(JumpDown &&!anim.GetBool("isJump")&& isInversion)
        {
            InversionJump();
        }

        
    }
    void Jump()
    {
            Boost = Instantiate(Resources.Load("Cloud"), transform.position, transform.rotation) as GameObject;
            rigid.AddForce(Vector3.up * JumpPulse, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            SoundManager.instance.SfXPlay("Jump", JumpClip);
        
    }

    void InversionJump()
    {
        Boost = Instantiate(Resources.Load("Cloud"), transform.position, transform.rotation) as GameObject;
        rigid.AddForce(Vector3.down * JumpPulse, ForceMode2D.Impulse);
        anim.SetBool("isJump", true);
        SoundManager.instance.SfXPlay("Jump", JumpClip);
    }

    void Landing()
    {
        if (rigid.velocity.y < 0)
        {

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));//레이어 이름에 해당하는 정수값을 리턴

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.4f)
                {
                    anim.SetBool("isJump", false);
                    canDoubleJump = false;
                }
            }
        }

        else if (rigid.velocity.y > 0)
        {

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.up, 0.5f, LayerMask.GetMask("Ground"));//레이어 이름에 해당하는 정수값을 리턴

            if (rayHit.collider != null)
            {
                if (rayHit.distance > -0.4f)
                {
                    anim.SetBool("isJump", false);
                    canDoubleJump = false;
                }
            }
        }
    }
    public void CollectCoin()
    {
        // 코인을 먹은 후 추가 점프 가능하도록 설정
        canDoubleJump = true;
    }
    #endregion

    #region 캐릭터 크기

    void FScale()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(1, 1, 1);
        startTime = Time.time;
    }
    public void CharacterScale()
    {
        isScale = true;
        // 현재 시간에서 시작 시간을 뺀 값을 duration으로 나누어 보간값을 계산합니다.
        float t = (Time.time - startTime) / duration;
        // Mathf.Clamp01 함수를 사용하여 t가 0에서 1 사이에 있도록 합니다.
        t = Mathf.Clamp01(t);
        // 보간값을 사용하여 스케일을 변경합니다.
        transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
    }

    public void OriginalCharacterScale()
    {
        isScale = false;
        float t = (Time.time - startTime) / duration;
        t = Mathf.Clamp01(t);
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, t);
    }
    #endregion

    #region 중력반전
    public void GravityInversionTrue()
    { 
            isInversion = true;
            spriteRenderer.flipX = true;
            rigid.gravityScale = -3f;
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
    public void GravityInversionFalse()
    {
        isInversion = false;
        spriteRenderer.flipX = false;
        rigid.gravityScale = 3f;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    #endregion
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
