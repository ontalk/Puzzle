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
    #endregion
    #region Ű ����
    private bool JumpDown;
    #endregion
    #region ���� ����
    private bool isMove = false;
    private bool isJump = false;
    private bool triggerBox; // ���� �Ⱥ��̴� ������ �΋H���� �ȴٸ� ��ֹ� ����.
    private bool isDamage = false;
    [NonSerialized] public bool isDead = false;
    #endregion
    #region Inspector ����
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public AudioClip DieClip;//�����
    public AudioClip JumpClip;//�����
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

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));//���̾� �̸��� �ش��ϴ� �������� ����

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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //�� �ٽ� �ε�
    }

}
