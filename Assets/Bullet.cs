using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_VerticalSpeed = 4;
    [SerializeField] float m_HorizontalSpeed = 0;
    [SerializeField] Camera m_MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }
    // Start is called before the first frame update
    void moveBullet()
    {
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(this.transform.position);
        this.transform.Translate( 0,Time.deltaTime * m_VerticalSpeed, 0);
        this.transform.Translate(Time.deltaTime * m_HorizontalSpeed, 0, 0);
        // If bullet is above top of the screen, delete bullet
        if(screenPos.y>m_MainCamera.pixelHeight) {
            Destroy(gameObject);
        }
    }

    public event Action OnHit;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            if(OnHit != null)
            {
                OnHit();
            } else {
                Debug.Log("No subscriber to event (Bullet)");
            }

            Destroy(gameObject);
        }    
    }
}
