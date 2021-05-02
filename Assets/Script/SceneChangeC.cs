using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeC : MonoBehaviour
{
    //성 맵에서의 방향키로 씬 전환 코드입니다. 기존 방법으로는 게임 오버 씬이 나오게 되서 수정하였습니다. 

    void Update()
    {
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LoadePreScene();
        }
    }

    void LoadePreScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        int preScene = curScene - 1;
        SceneManager.LoadScene(preScene);
    }
}
