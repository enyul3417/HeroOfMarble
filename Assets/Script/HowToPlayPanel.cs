using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

/*
 * 타이틀에서 설명 창이 나타날때 사용한 코드 입니다
*/

public class HowToPlayPanel : MonoBehaviour
{
    public GameObject HowToGame;
    public GameObject Close;
    // Start is called before the first frame update
    void Start()
    {
        HowToGame.SetActive(false);
        Close.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnPanel() // 판넬을 나타나게 함. 
    {
        HowToGame.SetActive(true);
        Close.SetActive(true);
    }

    public void OffPanel()
    {
        Close.SetActive(false);
        HowToGame.SetActive(false);
    }
}
