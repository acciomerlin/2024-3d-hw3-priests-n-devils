using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role
{
    public GameObject role; //根据属性生成的游戏对象
    public bool isPriest;
    public bool inBoat;
    public bool onLeft; //是否抵达左侧
    public int id;

    public Role(Vector3 position, bool isPriest, int id)
    {
        //根据传入属性创建游戏实例
        this.isPriest = isPriest;
        this.id = id;
        onLeft = false;
        inBoat = false;
        role = GameObject.Instantiate(Resources.Load("Prefabs/" + (isPriest ? "priest" : "devil"), typeof(GameObject))) as GameObject;
        //给游戏实例作一些处理
        // role.transform.localScale = new Vector3(1,1.2f,1);
        role.transform.position = position;
        role.name = "role" + id;
        //role.transform.eulerAngles = new Vector3(0, 180, 0);
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
    }
}
