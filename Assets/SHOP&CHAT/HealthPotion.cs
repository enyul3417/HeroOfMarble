using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/*
// 포션으로 회복하는 .....
[CreateAssetMenu(fileName ="HelathPotion",menuName ="Items/Potion",order =1)]
public class HealthPotion :Item
{
    [SerializeField]
    private int health;

    public object Player { get; private set; }

    public void Use()  //아이템 사용시 
    {
        // 체력이 최대 체력보다 낮으면
        if(Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        
        //사용하는 아이템 없애고
        Remove();

        //체력 회복
        PlayerController.MyInstance.MyHealth.MyCurrentValue += health;
    }

    private void Remove()
    {
        throw new NotImplementedException();
    }
}
*/