using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object
{
    static SSDirector _instance;
    public ISceneController CurrentSceneController { get; set; } //用来存储和访问当前场景控制器（实现了 ISceneController 接口的对象）
    //单例模式，全局场景控制器
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
}