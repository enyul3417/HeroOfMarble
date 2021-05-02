using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
   /*
    * 씬전환 관련 코드입니다. 
    */

   public void GameStart() // 처음 시작 메뉴에서 게임시작
    {
        //씬전환
        SceneManager.LoadScene("ScenarioScene");
    }

    public void Exit() // 처음 시작 메뉴에서 게임 종료 
    {

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
    #else
         Application.Quit();
    #endif
        }

        public void NextScenario()
    {
         SceneManager.LoadScene("Scenario2");
    }

    public void Village() // 마을로 이동 
    {
        SceneManager.LoadScene("VillageScene");
    }

    

}
