using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   
    private void Start()
    {
        
        SoundManager.instance.PlaySE("Title"); //시작 BGM
    
    }

    // 방향키로 씬 전환 코드입니다. 

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 5) // 성 씬이 아닐때만 오른쪽 방향키를 눌렀을때 넘어가도록 바꿔보았습니다. 
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                LoadNextScene();
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LoadePreScene();
        }

    }

    public void LoadNextScene()
    {

        int curScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = curScene + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadePreScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        int preScene = curScene - 1;
        SceneManager.LoadScene(preScene);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") // 플레이어가 
        {
            if (this.gameObject.tag == "Portal") // Portal 테그에 닿으면
            {
                if (Input.GetKey(KeyCode.W)) 
                    LoadePreScene();  // 이전씬으로
            }
            if (this.gameObject.tag == "Portal2")
            {
                if (Input.GetKey(KeyCode.W)) 
                    LoadNextScene(); // 다음씬으로
            }
            if (this.gameObject.tag == "EndPortal")
            {
                Debug.Log("EndPortal");
                if (Input.GetKey(KeyCode.W))
                    if (MarbleManager.getCount() >= 2) // 구슬 조각을 2개 이상 모았는가
                    {
                        SceneManager.LoadScene("GameClear");  // 엔딩씬 넘어가기
                        SoundManager.instance.PlaySE("환호와 박수"); //클리어 효과음
                       
                    }
                 
            }
        }
        
    }
}
