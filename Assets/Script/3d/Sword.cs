using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Debug.Log("1");
            Enemy scEnemy = other.gameObject.GetComponent<Enemy>();
            scEnemy.hit(1f);
        }

    }

}
