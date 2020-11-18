using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] GameObject m_GameOver;
    [SerializeField] Button m_Button;
    [SerializeField] Text m_Score;
    [SerializeField] Slider m_Health;
    [SerializeField] Player m_Player;

    void Awake()
    {
        m_Score = GameObject.Find("Score").GetComponent<Text>();
        m_Button = GameObject.Find("RetryButton").GetComponent<Button>();
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_Health = GameObject.Find("HealthBar").GetComponent<Slider>();
        m_Player.OnHPChange += ChangeHPBar;
        m_Player.OnScoreChange += ChangeScoreTxt;
        // m_Button.onClick += GameManager.Instance.ResetLevel;
        m_Button.onClick.AddListener(GameManager.Instance.ResetLevel);
    }

    
    void ChangeHPBar(int hp) {
        // m_Health.transform.localScale = new Vector3(-0.01f,0,0);
        m_Health.value = hp;
    }

    void ChangeScoreTxt(int score) {
        m_Score.text = score.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
