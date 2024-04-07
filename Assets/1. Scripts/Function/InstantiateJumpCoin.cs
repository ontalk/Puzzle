using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateJumpCoin : MonoBehaviour
{
    private static InstantiateJumpCoin _instance;

    public static InstantiateJumpCoin Instance
    {
        get { return _instance; }
    }
    public GameObject JumpPrefab;
    public bool hasSpawned = false;
    public Vector2 vectorPosition;
    public float Second;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(JumpCoin());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator JumpCoin()
    {
        while (true)
        {
            // JumpPrefab�� null�� �ƴϰ�, ���� �������� �ʾҰų� ������ ��쿡�� �������� �����մϴ�.
            if (!hasSpawned || JumpPrefab == null)
            {
                if (JumpPrefab != null)
                {
                    Instantiate(JumpPrefab, vectorPosition, Quaternion.identity);
                    hasSpawned = true;
                }
            }
            else if (JumpPrefab == null)
            {
                // JumpPrefab�� �����Ǿ��ٸ� hasSpawned�� false�� �����Ͽ� �ٽ� ������ �� �ֵ��� �մϴ�.
                hasSpawned = false;
            }
            yield return new WaitForSeconds(Second); // 1�ʸ��� Ȯ���մϴ�
        }

    }
}
