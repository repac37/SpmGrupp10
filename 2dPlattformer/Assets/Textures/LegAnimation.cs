using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LegAnimation : MonoBehaviour {

    public Animator anim;
    //int jumpHash = Animator.StringToHash("Jump");
    //int runStateHash = Animator.StringToHash("Base Layer.Run");
    public float ChangeTime;
    public float time;
    private PlayerManager p;
    public AudioClip walk;
    public AudioSource src;
    public AudioMixerGroup output;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        time = ChangeTime;
        p = FindObjectOfType<PlayerManager>();

    }

    // Update is called once per frame
    void Update() {

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", move);

        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //if (Input.GetKeyDown(KeyCode.Space) & stateInfo.nameHash == runStateHash)
        //{
        //  anim.SetTrigger(jumpHash);
        //Debug.Log(Input.GetButtonDown("LeftBumper"));
        if ((Input.GetAxis("LeftTrigger") != 0 && p.jetPack==1) || (Input.GetButtonDown("LeftBumper") && p.jetPack==1))
        {
            time = ChangeTime;
            anim.SetBool("Jump", true);
        }
        if (Input.GetAxis("LeftTrigger") == 0 && time<=0)
        {

            anim.SetBool("Jump", false);
        }
        if(time>0)
        time -= Time.deltaTime;

        //}
    }
    public void playSound()
    {
        src.PlayOneShot(walk);
    }

}
