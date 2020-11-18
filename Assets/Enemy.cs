using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // [SerializeField] float m_Speed = 0.01f;
    [SerializeField] float m_Speed = 0;
    Camera m_MainCamera;

    new void Awake()
    {
        m_MainCamera = Camera.main;
        this.m_curPV = 200;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(this.transform.position);
        // Debug.Log("Enemy in position " + screenPos);
        this.transform.Translate( 0,-1 * Time.deltaTime * m_Speed, 0);
        
        // If enemy is below the screen, delete enemy
        if(screenPos.y<0) {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            this.m_curPV -= 100;
            if(this.m_curPV <= 0)
            {
                Destroy(gameObject);
            }
        } else {
                Destroy(gameObject);
        }
    }
}
