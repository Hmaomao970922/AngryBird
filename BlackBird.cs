using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : bird
{
    public List<pig> blocks = new List<pig>();

    //进入爆炸范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<pig>());
        }
    }
    //离开爆炸范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<pig>());
        }
    }

    protected override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count > 0 && blocks != null)
        {
            for(int i=0;i<blocks.Count;i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(bomb, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    protected override void Next()
    {
        base.Next();
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager.Instance.NextBird();
    }

}
