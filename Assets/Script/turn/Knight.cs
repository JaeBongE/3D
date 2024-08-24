using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private bool doAttack = false;
    private GameObject objTarget;
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        checkTarget();
    }

    public void Attack(bool _isAttack)
    {
        if (_isAttack == true)
        {
            //Vector3 posKnight = gameObject.transform.position;

            //gameObject.transform.position = new Vector3(posKnight.x + 1 , posKnight.y, posKnight.y);
            Debug.Log("click target");
            doAttack = true;
        }
    }

    private void checkTarget()
    {
        if (doAttack == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("check target");

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, LayerMask.GetMask("Enemy")))
            {
                Debug.Log("doAttack");
            }
            doAttack = false;
        }
    }
}
