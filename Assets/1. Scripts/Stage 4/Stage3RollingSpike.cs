using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3RollingSpike : MonoBehaviour
{
    public string Stage;
    private Rigidbody2D rb;
    public float rotationSpeed = 360f; // 초당 회전 속도
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotationSpeed; // 초기 회전 속도 설정
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = rotationSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(Stage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            gameObject.SetActive(false);
        }
    }
}
