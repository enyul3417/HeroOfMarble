using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect2 : MonoBehaviour
{
    public Text tx;
    public GameObject panel;

    private string m_text = "예상치 못한 습격으로 구슬은 산산조각이나 흩어지게 되었다. 구슬이 사라진 왕국에는 큰 혼란이 찾아왔고, 왕국은 순식간에 아수라장이 되었다. 이때 한 용사가, 구슬을 되돌리기 위해, 구슬 조각을 찾아 길을 나섰다.";
    AudioSource typingSound; // AudioSource 컴포넌트를 변수로 담기
    public static TypingEffect2 instance; // 자기자신을 변수로 담기

    private void Awake() // Start보다 먼저, 객체가 생성될때 호출
    {
        if (TypingEffect2.instance == null) // instance가 비어있다면
            TypingEffect2.instance = this; // 자기 자신을 담기
    }

    private void Start()
    {
        panel.SetActive(false);
        typingSound = this.gameObject.GetComponent<AudioSource>(); // AudioSource 오브젝트를 변수로 담기
        StartCoroutine(_typring()); // 시나리오가 타이핑 됨.
       
    }

    IEnumerator _typring()
    {
        yield return new WaitForSeconds(0.2f);
        typingSound.Play(); // typingSound를 재생
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);

            if (i == m_text.Length)
            {
                panel.SetActive(true);
            }

            yield return new WaitForSeconds(0.095f); // 재생 속도
             
        }
        typingSound.Stop(); // 글자 입력이 끝나면 typingSound 멈추기
    }
}
