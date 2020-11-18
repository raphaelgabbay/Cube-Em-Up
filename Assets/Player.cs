using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] GameObject m_Bullet;
    [SerializeField] float m_VerticalSpeed = 2;
    [SerializeField] float m_HorizontalSpeed = 2;
    [SerializeField] float m_ShootingCadency = 500; //In ms
    Stopwatch m_stopWatch;
    int m_score;
    GameObject gameover;
    GameObject retrybtn;

    public override void Awake() {
        this.m_curPV = this.m_maxPV;
        if (OnHPChange != null)
            {
                OnHPChange(this.m_curPV);
            } else {
                Debug.Log("No subscriber to event (HP)");
            }
        m_MainCamera = Camera.main;
        m_Bullet = Resources.Load("Bullet") as GameObject;
        m_stopWatch = new Stopwatch();
        m_stopWatch.Start();
        m_score = 0;
        gameover = GameObject.Find("GameOver");
        retrybtn = GameObject.Find("RetryButton");
        gameover.SetActive(false);
        retrybtn.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        // Debug.Log(m_stopWatch.ElapsedMilliseconds);
    }

    void PlayerControl(){
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(this.transform.position);
        if (Input.GetKey("right") && screenPos.x<m_MainCamera.pixelWidth)
        {
            // print("right arrow key is held down");
            this.transform.Translate(Time.deltaTime * m_HorizontalSpeed, 0, 0);
            // this.transform.Translate(Time.deltaTime * 1, 0, 0);
        }

        if (Input.GetKey("left") && screenPos.x>0)
        {
            // print("left arrow key is held down");
            this.transform.Translate(-1 * Time.deltaTime * m_HorizontalSpeed, 0, 0);
            // this.transform.Translate(-1 * Time.deltaTime * 1, 0, 0);
        }

        if (Input.GetKey("up") && screenPos.y<m_MainCamera.pixelHeight)
        {
            // print("up arrow key is held down");
            this.transform.Translate( 0,Time.deltaTime * m_VerticalSpeed, 0);
            // this.transform.Translate(-1 * Time.deltaTime * 1, 0, 0);
        }

        if (Input.GetKey("down") && screenPos.y>0)
        {
            // print("down arrow key is held down");
            this.transform.Translate( 0,-1 * Time.deltaTime * m_VerticalSpeed, 0);
            // this.transform.Translate(-1 * Time.deltaTime * 1, 0, 0);
        }

        if (Input.GetKey("space") && m_stopWatch.ElapsedMilliseconds >= m_ShootingCadency)
        {
            // Debug.Log(m_ShootingCadency);
            // Debug.Log(m_stopWatch);
            // Create Bullet instance at Player's position
            GameObject myBulletObj = Instantiate(m_Bullet, transform.position, transform.rotation);
            Bullet myBullet = myBulletObj.GetComponent<Bullet>();
            myBullet.OnHit += OnBulletHit;
            m_stopWatch.Reset();
            m_stopWatch.Start();
        }
    }

    void OnBulletHit()
    {
        Debug.Log("Bullet collided!");
        Debug.Log("Score : " + this.m_score);
        this.m_score++;
        if(OnScoreChange != null)
        {
            OnScoreChange(this.m_score);
        } else {
            Debug.Log("No subscriber to event (Score)");
        }
        // OnScoreChange();
    }
    
    
    public event Action<int> OnHPChange;
    // OnHPChange = ChangeHPBar;
    
    public event Action<int> OnScoreChange;
    // OnScoreChange = this.m_score;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            this.m_curPV -= 100;
            if(OnHPChange != null)
            {
                OnHPChange(this.m_curPV);
            } else {
                Debug.Log("No subscriber to event (HP)");
            }
            Debug.Log(this.m_curPV);
            if(this.m_curPV <= 0)
            {
                Debug.Log("Game Over");
                // GameObject.Find("RetryButton").GetComponent<Button>().enabled = true;
                // GameObject.Find("GameOver").GetComponent<Text>().enabled = true;
                gameover.SetActive(true);
                retrybtn.SetActive(true);
                Destroy(gameObject);
            }

        }
    }
}
