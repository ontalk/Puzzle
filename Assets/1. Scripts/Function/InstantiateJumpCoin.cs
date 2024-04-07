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
            // JumpPrefab이 null이 아니고, 아직 생성되지 않았거나 삭제된 경우에만 프리팹을 생성합니다.
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
                // JumpPrefab이 삭제되었다면 hasSpawned를 false로 설정하여 다시 생성할 수 있도록 합니다.
                hasSpawned = false;
            }
            yield return new WaitForSeconds(Second); // 1초마다 확인합니다
        }

    }
}
