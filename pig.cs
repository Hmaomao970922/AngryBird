using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;

    private SpriteRenderer render;
    public Sprite hurt;

    public GameObject bomb;
    public GameObject score;

    public AudioClip dead;
    public AudioClip hit;
    public AudioClip birdCollision;

    public bool isPig = false;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
       
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > maxSpeed)
        {
            collision.transform.GetComponent<bird>().Hurt();
            //死亡
            Dead();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed&&collision.relativeVelocity.magnitude < maxSpeed)
        {
            
            //受伤
            render.sprite = hurt;
            //播放音效
            AudioPlay(hit);
            if(collision.gameObject.tag == "Player")
            {
                AudioPlay(birdCollision);
            }
        }
    }
    public void Dead()
    {

        if(isPig)
        {
            GameManager.Instance.pig.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(bomb, transform.position, Quaternion.identity);
        //播放音效
        AudioPlay(dead);
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
