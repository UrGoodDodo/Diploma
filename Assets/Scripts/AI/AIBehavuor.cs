using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class AIBehavuor : MonoBehaviour
{

    //Component of AI navigation
    static public NavMeshAgent ai_nav;
    //Component of player
    public Transform player;
    //AI`s eyes
    public Transform ai_eyes;
    //Position of AI
    public Transform ai_position;
    //AI speed
    public float ai_speed;

    //Player`s position
    Vector3 dest;
    //Angle of AI view
    [Range(0f, 360f)] float view_angel = 350f;
    //View distance AI
    float view_dist = 15f;
    //Detection distance AI
    float detection_dist = 2f;
    //
    //private float rotation_speed_ai;
    //
    //
    Animator anim;
    //
    float timer;
    //
    public static bool is_searching_key;

    public static bool key_was_found;

    static public List<Transform> points = new List<Transform>();

    static public int search_number = 0;

    static public bool is_helping;

    static public bool is_the_end = false;

    public static Action IsHelping;

    public static bool is_start;

    // Start is called before the first frame update
    void Start()
    {
        ai_nav = GetComponent<NavMeshAgent>();
        timer = 0;
        if (ai_speed == 0)
            ai_speed = 2f;
        else
            ai_nav.speed = ai_speed;
        anim = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag($"Points{search_number}"))
        {
            Transform pointObject = GameObject.FindGameObjectWithTag($"Points{search_number}").transform;
            if (pointObject != null)
            {
                foreach (Transform p in pointObject)
                {
                    points.Add(p);
                }
                ai_nav = anim.GetComponent<NavMeshAgent>();
                ai_nav.SetDestination(points[0].position);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float real_dist_to_player = Vector3.Distance(player.transform.position, ai_position.transform.position);
        //if (is_the_end)
        //{
        //    The_End();
        //}
        //else
        if (is_searching_key)
        {
            SearchingKeyAI();
        }
        else if (is_helping)
        {
            //Debug.Log("Help");
            HelpAI();
        }
        else
        {
            if (real_dist_to_player <= detection_dist || IsInView())
            {
                //RotationAI();
                if (real_dist_to_player <= 1.8f)
                    Wait();
                else
                {
                    Follow();
                }
            }
        }
        
    }

    //State of following AI
    protected void Follow()
    {
        if (is_start)
        {
            if (ai_nav.isStopped)
                ai_nav.Resume();
            // RotationAI();
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsIdle", false);
            anim.Play("walking");
            dest = player.position;
            ai_nav.destination = dest;
        }
    }

    //State of waiting AI
    [System.Obsolete]
    protected void Wait()
    {
        ai_nav.Stop();
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsIdle", true);
        anim.Play("idle");
    }

    //Player is in the field of view of AI
    private bool IsInView()
    {
        float real_angle = Vector3.Angle(ai_eyes.forward, player.position - ai_eyes.position);
        RaycastHit hit;
        if(Physics.Raycast(ai_eyes.transform.position, player.position - ai_eyes.position, out hit, view_dist))
        {
            if (real_angle < view_angel / 2f && Vector3.Distance(ai_eyes.position, player.position) <= view_dist && hit.transform == player.transform)
                return true;
        }
        return false;
    }

    //Rotation AI
    private void RotationAI()
    {
        Vector3 look = player.position - ai_position.position;
        look.y = 0;
        if (look == Vector3.zero)
            return;
        ai_position.rotation = Quaternion.RotateTowards(player.rotation, Quaternion.LookRotation(look, Vector3.up), 30 * Time.deltaTime);
    }

    private void SearchingKeyAI()
    {
        if(!anim.GetBool("IsSearching") && !anim.GetBool("IsWalking"))
            anim.Play("search");
    }

    private void HelpAI()
    {
        if (GameObject.FindGameObjectWithTag("PointFH"))
        {
            var p = GameObject.FindGameObjectWithTag("PointFH").transform;
            if(Vector3.Distance(p.transform.position, ai_position.transform.position) <= 1.0f)
            {
                Wait();
                IsHelping?.Invoke();
            }
            else
            {
                if (ai_nav.isStopped)
                    ai_nav.Resume();
                anim.SetBool("IsWalking", true);
                anim.Play("walking");
                dest = p.position;
                ai_nav.destination = dest;
            }
        }
        //если объект не нал, то сюда передаем значение после касания триггера, что прошло нное количество времени и собаке надо подойти к точке и посветить на объект
 
    }

    private void The_End()
    {
        ai_nav.transform.LookAt(player.transform);
        IsHelping?.Invoke();
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(5f);
    }

}
