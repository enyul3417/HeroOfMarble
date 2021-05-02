using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public Text tx;
    private string m_text = "오래 전부터 왕국에는 평화를 유지하는 구슬이 있었다. 하지만...";
    AudioSource typingSound; // AudioSource 컴포넌트를 변수로 담기
    public static TypingEffect instance; // 자기자신을 변수로 담기

    private void Awake() // Start보다 먼저, 객체가 생성될때 호출
    {
        if (TypingEffect.instance == null) // instance가 비어있다면
            TypingEffect.instance = this; // 자기 자신을 담기
    }


    private void Start()
    {
        typingSound = this.gameObject.GetComponent<AudioSource>(); // AudioSource 오브젝트를 변수로 담기
        StartCoroutine(_typring()); // 시나리오가 타이핑 됨. 
        
    }

    IEnumerator _typring()
    {
        yield return new WaitForSeconds(0.2f);
        typingSound.Play();// typingSound를 재생
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
         
            yield return new WaitForSeconds(0.095f); // 재생 속도

         
        }
        typingSound.Stop(); // 글자 입력이 끝나면 typingSound 멈추기
    }
}
