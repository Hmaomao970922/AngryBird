using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<bird> birds;
    public List<pig> pig;
    public static GameManager Instance;

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    //初始位置
    private Vector3 originPos;


    private int starsNum = 0;

    private int totalNum = 10;
    private void Awake()
    {
        Instance = this;
        if(birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
        
    }
    //初始化
    private void initialized()
    {
        for(int i=0;i<birds.Count;i++)
        {
            if(i == 0)//第一只
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;

            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }
    void Start()
    {
        initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //逻辑判断
   public void NextBird()
    {
        if(pig.Count > 0)
        {
            if(birds.Count > 0)
            {
                //下一只鸟
                initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }
    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if(starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }

    public void Replay()
         
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);

        }
        int sum = 0;
        //
        for(int i=1;i <= totalNum;i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum",sum);
    }
}
