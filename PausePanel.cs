using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{

    private Animator anim;
    public GameObject button;
    public GameObject Pausepanel;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    //点击了Pause
    public void Pause()
    {
        Pausepanel.SetActive(true);
        //播放Pause动画
        anim.SetBool("isPause", true);
        button.SetActive(false);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    //点击继续
    public void Resume()
    {
        
        Time.timeScale = 1;
        //播放Resume动画
        anim.SetBool("isPause", false);
    }
    //Pause动画播放完调用
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }
    //Rusume动画播放完调用
    public void ResumeAnimEnd()
    {
        button.SetActive(true);
        Pausepanel.SetActive(false);
    }
}
