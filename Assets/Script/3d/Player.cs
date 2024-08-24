using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{
    Camera mainCam;
    CharacterController characterController;

    private NavMeshAgent agent;
    private Animator anim;

    private Vector3 targetPos;

    PlayerState state;

    [SerializeField] private bool isMove = false;
    [SerializeField] private bool isAttack = false;

    [SerializeField] private Collider AttackRange;

    private GameObject targetEnemy;

    public enum PlayerState
    {
        idle,
        walk,
        attack,
    }



    private void Awake()
    {
        targetPos = transform.position;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        AttackRange.enabled = false;
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        move();
        doAnim();
    }

    private void move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, LayerMask.GetMask("Ground", "Enemy")))
            {
                //agent.isStopped = false;
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    isAttack = false;
                    isMove = true;
                    state = PlayerState.walk;
                    targetPos = hit.point;
                    transform.LookAt(targetPos);

                    Debug.Log("이동");
                    Debug.Log($"{targetPos.x}");
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    targetEnemy = hit.transform.gameObject;
                    isMove = true;
                    isAttack = true;
                    state = PlayerState.walk;
                    targetPos = hit.point;
                    transform.LookAt(targetPos);
                    Debug.Log("공격");
                    
                }
            }
        }

        if (isMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5f);
        }
        //agent.SetDestination(targetPos);

        if (state == PlayerState.walk && Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            if (isAttack == true) return;

            //agent.isStopped = true;
            //agent.velocity = Vector3.zero;
            state = PlayerState.idle;
            isMove = false;
            Debug.Log("도착");
        }

        if (isAttack == true && Vector3.Distance(transform.position, targetPos) <= 1.5f)
        {
            isMove = false;
            transform.LookAt(targetPos);
            state = PlayerState.attack;
            isAttack = false;
        }

        if (targetPos == null)
        {
            isMove = false;
            isAttack = false;
            state = PlayerState.idle;
        }

        if (state == PlayerState.attack && targetEnemy == null)
        {
            isMove = false;
            isAttack = false;
            state = PlayerState.idle;
        }

    }

    private void doAnim()
    {
        switch (state)
        {
            case PlayerState.walk:
                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
                //Debug.Log($"{state}");
                break;

            case PlayerState.idle:
                anim.SetBool("Walk", false);
                anim.SetBool("Attack", false);
                //anim.CrossFade("Idle", 0.2f);
                //Debug.Log($"{state}");
                break;

            case PlayerState.attack:
                anim.SetBool("Attack", true);
                //Debug.Log($"{state}");
                break;
        }


        //if (state == PlayerState.walk)
        //{
        //    anim.SetBool("Walk", true);
        //}
        //else if (state == PlayerState.idle)
        //{
        //    anim.SetBool("Walk", false);
        //    anim.CrossFade("Idle", 0.2f);
        //}
    }

    public void StartAttack()
    {
        AttackRange.enabled = true;
    }

    public void FinishAttack()
    {
        AttackRange.enabled = false;
    }
}
