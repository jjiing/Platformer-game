using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target_y;
    public GameObject target_p;



    private float moveSpeed; //ī�޶� �ӵ�


    private Vector3 targetPos_y;
    private Vector3 targetPos_p;
    private Vector3 targetPos; //��� ��ġ


    //ī�޶� �ٿ�� ����
    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;



    void Start()
    {
        moveSpeed = 1f;

        //ī�޶� �ٿ�� ����
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        Vector3 startPos = (target_y.transform.position + target_p.transform.position)/ 2;
        this.transform.position = new Vector3(startPos.x, startPos.y+2 , transform.position.z);


    }


    void Update()
    {
        //ī�޶� �̵�

        targetPos_y = target_y.transform.position;
        targetPos_p = target_p.transform.position;
        targetPos = (targetPos_y + targetPos_p) / 2;

        if (target_y.gameObject != null && target_p.gameObject != null)
        {
            targetPos.Set(targetPos.x, targetPos.y + 2, transform.position.z);
            this.transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }


        //ī�޶� �ٿ�� ����
        float clampedX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
