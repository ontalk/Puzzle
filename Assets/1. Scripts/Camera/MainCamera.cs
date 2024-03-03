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
            // 배경의 위치를 카메라의 위치로 맞추기
            BackgroundTransform.position = new Vector3(CameraTransform.position.x, CameraTransform.position.y, BackgroundTransform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {

            Vector3 targetPosition = Target.position;

            // 카메라 위치에 오프셋 추가
            Vector3 offset = new Vector3(0, 2, -3); // 필요에 따라 이 값을 조정하십시오
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
