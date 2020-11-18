using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour
{
    [SerializeField] public int m_maxPV = 1000;
    public int m_curPV;
    
    public virtual void Awake()
    {
        m_curPV = m_maxPV;
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
