using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    Camera mainCam;
    CharacterController characterController;

    private NavMeshAgent agent;
    private Animator anim;

    private Vector3 targetPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            transform.LookAt(other.gameObject.transform);
            anim.SetTrigger("Attack");
        }
    }

    private void Awake()
    {
        targetPos = transform.position;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        move();
        attack();
        doAnim();
    }

    private void move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, LayerMask.GetMask("Ground")))
            {
                targetPos = hit.point;
                transform.LookAt(targetPos);
                //transform.position = hit.point;
                //transform.position = Vector3.MoveTowards(transform.position, hit.point, 1f);

                //if (agent.stoppingDistance <= 0.5f)
                //{
                //    anim.SetBool("Walk", false);
                //    anim.CrossFade("Idle", 0.2f);
                //}
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5f);
    }

    private void attack()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, LayerMask.GetMask("Enemy")))
            {

            }
        }
    }


    private void doAnim()
    {
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.CrossFade("Idle", 0.2f);
        }
    }

}
