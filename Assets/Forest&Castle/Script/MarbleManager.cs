using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleManager : MonoBehaviour
{
    static int count = 0; // 구슬 조각 개수
    public Text countText;

    public static void setCount(int i)
    {
        count += i;
    }

    public static int getCount()
    {
        return count;
    }

    private void Update()
    {
        countText.text = count.ToString();
    }
}
