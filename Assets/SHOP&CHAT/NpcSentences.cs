using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentences : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    void Start()
    {
       Invoke("TalkNpc",5.0f);
    }

   
   public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences,chatTr);
        Invoke("TalkNpc", 10.0f);
    }
   
}
