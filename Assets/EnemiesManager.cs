using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject m_Enemy;
    [SerializeField] float m_ApparitionDelay = 1; //In seconds
    Camera m_MainCamera;

    void Awake()
    {
        m_MainCamera = Camera.main;
        m_Enemy = Resources.Load("Enemy") as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start function WaitAndSpawnEnemy as a coroutine.
        // Debug.Log("Coroutine about to start");
        StartCoroutine(WaitAndSpawnEnemy(m_ApparitionDelay));
        // Debug.Log("Coroutine started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndSpawnEnemy(float apparitionDelay){
        while(Application.isPlaying) {            
            // Debug.Log("Waiting for " + apparitionDelay + " seconds");
            yield return new WaitForSeconds(apparitionDelay);
            // Debug.Log("Spawning enemy");
            Instantiate(m_Enemy, m_MainCamera.ScreenToWorldPoint(new Vector3(Random.Range(0,m_MainCamera.pixelHeight),m_MainCamera.pixelWidth,-1 * m_MainCamera.transform.position.z)), Quaternion.Euler(0, 0, 0));
            // Instantiate(m_Enemy, new Vector3(Random.Range(0,m_MainCamera.pixelHeight),0,0), Quaternion.Euler(0, 0, 0));
            // Instantiate(m_Enemy, new Vector3(Random.Range(0,m_MainCamera.pixelWidth),m_MainCamera.pixelHeight,0), Quaternion.Euler(0, 0, 0));
        }
    }
}
