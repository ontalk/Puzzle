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
    public Vector3 MoveTo; // ĳ���� �̵� ������ ��Ÿ���� ����
    private float Horizontal_Move; // ���� �Է� (�¿�)
    private float Vertical_Move; // ���� �Է� (����)

    #region �� ����
    public float JumpPulse = 10; // ������
    private float currentMoveSpeed; // ���� �̵� �ӵ�
    [SerializeField] private float initialMoveSpeed = 5f; // �ʱ� �̵� �ӵ�
    [SerializeField] private float maxMoveSpeed = 13f; // �ִ� �̵� �ӵ�
    public float duration = 0.5f;
    private float startTime;
    #endregion
    #region Ű ����
    private bool JumpDown;
    #endregion
    #region ���� ����
    private bool isMove = false;
    bool isJump = false;
    public bool canDoubleJump = false;
    public bool isScale = false;
    private bool triggerBox; // ���� �Ⱥ��̴� ������ �΋H���� �ȴٸ� ��ֹ� ����.
    private bool isDamage = false;
    public bool isInversion = false;
    [NonSerialized] public bool isDead = false;
    [NonSerialized] public bool isHorizontalInversion = false;
    #endregion
    #region Inspector ����
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public GameObject Boost;
    public AudioClip DieClip;//�����
    public AudioClip JumpClip;//�����
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

    #region ������
    private void DirectionMove()
    {
        #region �߷¹��� X
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
        #region �߷¹��� O
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
            Horizontal_Move = Input.GetAxisRaw("Horizontal"); // ���� �Է� (�¿�)
            rigid.AddForce(Vector2.right * Horizontal_Move, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxMoveSpeed) //������
                rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxMoveSpeed * (-1)) //����
                rigid.velocity = new Vector2(maxMoveSpeed * (-1), rigid.velocity.y);
            if (rigid.velocity.normalized.x == 0)
                anim.SetBool("isWalk", false);
            else 
                anim.SetBool("isWalk", true);
            isMove = true;
        }
        else if(isHorizontalInversion)
        {
            Horizontal_Move = Input.GetAxisRaw("InversionHorizontal"); // ���� �Է� (�¿�)
            rigid.AddForce(Vector2.right * Horizontal_Move, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxMoveSpeed) //������
                rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxMoveSpeed * (-1)) //����
                rigid.velocity = new Vector2(maxMoveSpeed * (-1), rigid.velocity.y);
            if (rigid.velocity.normalized.x == 0)
                anim.SetBool("isWalk", false);
            else
                anim.SetBool("isWalk", true);
            isMove = true;
        }
    }

    #endregion

    #region ����
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

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));//���̾� �̸��� �ش��ϴ� �������� ����

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

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.up, 0.5f, LayerMask.GetMask("Ground"));//���̾� �̸��� �ش��ϴ� �������� ����

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
        // ������ ���� �� �߰� ���� �����ϵ��� ����
        canDoubleJump = true;
    }
    #endregion

    #region ĳ���� ũ��

    void FScale()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(1, 1, 1);
        startTime = Time.time;
    }
    public void CharacterScale()
    {
        isScale = true;
        // ���� �ð����� ���� �ð��� �� ���� duration���� ������ �������� ����մϴ�.
        float t = (Time.time - startTime) / duration;
        // Mathf.Clamp01 �Լ��� ����Ͽ� t�� 0���� 1 ���̿� �ֵ��� �մϴ�.
        t = Mathf.Clamp01(t);
        // �������� ����Ͽ� �������� �����մϴ�.
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

    #region �߷¹���
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //�� �ٽ� �ε�
    }
}
