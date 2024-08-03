using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHp = 5;
    [SerializeField] private float curHp;



    private void Awake()
    {
        curHp = maxHp;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (curHp < 1)
        {
            Destroy(gameObject);
        }
    }

    public void hit(float _damage)
    {
        curHp =-_damage;
    }
}
