using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class AIBehavuor : MonoBehaviour
{

    //Component of AI navigation
    public NavMeshAgent ai_nav;
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
    [Range(0f, 360f)] float view_angel = 120f;
    //View distance AI
    float view_dist = 15f;
    //Detection distance AI
    float detection_dist = 2f;
    //
    //private float rotation_speed_ai;
    //
    //
   // public Animator anim;
    //
    float timer;
    //
    public static bool is_searching_key;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        if (ai_speed == 0)
            ai_speed = 2f;
        else
            ai_nav.speed = ai_speed;
        //anim = GetComponent<Animator>();
        //anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
        float real_dist_to_player = Vector3.Distance(player.transform.position, ai_position.transform.position);
        if (is_searching_key)
        {
            SearchingKeyAI();
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
        if(ai_nav.isStopped)
            ai_nav.Resume();
        // RotationAI();
       // anim.SetBool("IsWalking", true);
       // anim.Play("Walk");
        dest = player.position;
        ai_nav.destination = dest;
    }

    //State of waiting AI
    [System.Obsolete]
    protected void Wait()
    {

        ai_nav.Stop();
      //  anim.SetBool("IsWalking", false);
       // StartCoroutine(StopTime());
       // anim.Play("Idle");
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
      //  anim.SetBool("IsSearching", true);
       // anim.Play("Search");
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(5f);
    }

}
