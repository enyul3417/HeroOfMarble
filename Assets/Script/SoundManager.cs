using UnityEngine;
using System.Collections;



[System.Serializable]
public class Sound{
    public string soundName;
    public AudioClip clip;     //MP3
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;   //클래스 자체를 Static변수로 만들어 어디서든 참조가 가능하도록 하기 위해


    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;   //bgm의 종류를 여러개로 선택할 수 있기 때문에 배열 선택
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;  //효과음은 동시에 재생될 수 있기때문에 배열 할당

    //효과음 재생 함수
    public void PlaySE(string _soundName) 
     {
         for(int i=0; i<sfxSounds.Length; i++)
         {
             if(_soundName==sfxSounds[i].soundName)
             {
                 for(int x=0; x<sfxPlayer.Length; x++)
                 {
                     if (!sfxPlayer[x].isPlaying)
                     {  //x번째의 MP3플레이어가 재생중이지 않다면
                         sfxPlayer[x].clip = sfxSounds[i].clip; //재생중이지 않은 x번째 플레이어에 i번째 MP3넣어줌
                         sfxPlayer[x].Play();
                         return;    //원하는 걸 찾았으므로 반복문 나가기
                     }
                 }
                Debug.Log("모든 효과음 플레이어가 사용중입니다!");
                return;

             }
         }
        Debug.Log("등록된 효과음이 없습니다");
     }


   // start is called beore the first frame update
    void Start()
    {
        instance = this;
        //PlayRandomBGM();
        DontDestroyOnLoad(transform.gameObject); // 다음 scene으로 넘어가도 오브젝트가 사라지지 않습니다.
    }


    public void PlayRandomBGM()
     {
        int random = Random.Range(0,2);    //정수 타입일 때 MAX값 미포함, 실수 타입일 때 MAX값 포함
        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.Play();
        
     }

    //SoundManager.instance.playSE("사운드이름");

    //연속으로 사용하니까 효과음이 묻히는 경우 발생,
    //가용할 수 있는 AudioSorce를 추가함으로써 해결할 수 있었다.


}
