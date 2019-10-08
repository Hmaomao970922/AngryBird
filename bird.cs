using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{

    protected bool isClick = false;

    public float smooth = 3;
   
    public float maxDis = 3;
    [HideInInspector]
    public SpringJoint2D sp;

    protected Rigidbody2D rg;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject bomb;

    private bool canMove = true;

    public AudioClip select;
    public AudioClip fly;

    protected bool isfly = false;

    public Sprite hurt;
    protected SpriteRenderer render;
    protected virtual void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    //鼠标按下
    protected virtual void OnMouseDown()
    {
        AudioPlay(select);
        if(canMove)
        {
            isClick = true;
            rg.isKinematic = true;
        }
        
    }
    //鼠标抬起
    protected virtual void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用画线
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
    }
    void Start()
    {
      
    }

    void Update()
    {
        //鼠标按下,位置跟随
        if(isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            //位置限定
            if (Vector3.Distance(transform.position,rightPos.position) > maxDis)
            {
                //单位化向量
                Vector3 pos = (transform.position - rightPos.position).normalized;
                //最大长度向量
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        //相机跟随
        float posx = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posx,0,17),Camera.main.transform.position.y,Camera.main.transform.position.z),smooth * Time.deltaTime);

        if(isfly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    protected virtual void Fly()
    {
        isfly = true;
        AudioPlay(fly);
        sp.enabled = false;
        Invoke("Next", 5f);
    }
    //画线
    protected virtual void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    //下一只鸟
    protected virtual void Next()
    {
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(bomb, transform.position, Quaternion.identity);
        GameManager.Instance.NextBird();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        isfly = false;

    }

    protected virtual void ShowSkill()
    {
        isfly =false;
    }

    //受伤
    public void Hurt()
    {

        render.sprite = hurt;
    }
}
