using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MainCamera : MonoBehaviour
{
    public Transform Target;
    public Transform CameraTransform;
    public Transform BackgroundTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (CameraTransform == null)
            CameraTransform = Camera.main.transform;
        //DontDestroyOnLoad(this);
        FindSetTarget();
    }

    void LateUpdate()
    {
        if (CameraTransform != null && BackgroundTransform != null)
        {
            // ����� ��ġ�� ī�޶��� ��ġ�� ���߱�
            BackgroundTransform.position = new Vector3(CameraTransform.position.x, CameraTransform.position.y, BackgroundTransform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {

            Vector3 targetPosition = Target.position;

            // ī�޶� ��ġ�� ������ �߰�
            Vector3 offset = new Vector3(0, 2, -3); // �ʿ信 ���� �� ���� �����Ͻʽÿ�
            transform.position = targetPosition + offset;

            transform.LookAt(Target.position);
        }else if(Target == null)
        {
            FindSetTarget();
        }


    }

    void FindSetTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
         SetTarget(player.transform);   
        }
    }
    void SetTarget(Transform newTarget) 
    {
        Target = newTarget;
    }
}
