using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat
{
    public GameObject boat;
    public Role[] roles;
    public bool isLeft;
    public int priestCount, devilCount; //船上的牧师和恶魔数量

    public Boat(Vector3 position)
    {
        boat = GameObject.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject))) as GameObject;
        boat.name = "Boat";
        boat.transform.position = position;
        //boat.transform.localScale = new Vector3(2.8f, 0.4f, 2);

        roles = new Role[2];        // 船上最多两个角色
        isLeft = false;         // 初始在右边
        priestCount = devilCount = 0;      // 初始牧师和魔鬼数量为0

        boat.AddComponent<BoxCollider>();              // 添加碰撞组件
        boat.AddComponent<Click>();                    // 添加点击组件 
    }
}
