using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player"); // 플레이어 찾으라고 알려주기
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position; // 플레이어 위치 알려주기
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z); // 카메라가 플레이어 따라다님
    }
}
