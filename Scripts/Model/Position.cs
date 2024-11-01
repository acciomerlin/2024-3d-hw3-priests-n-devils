using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position  //存储所有对象的位置
{
    //固定位置（世界坐标）
    public static Vector3 river = new Vector3(0, 0, 0);
    public static Vector3 left_shore = new Vector3(0, 35, -443);
    public static Vector3 right_shore = new Vector3(220, 35, -443);
    public static Vector3 right_boat = new Vector3(130, -303, -345);
    public static Vector3 left_boat = new Vector3(-211, -303, -426);

    //角色相对于岸边的位置(相对坐标)
    public static Vector3[] right_role_shore = new Vector3[] { new Vector3(51, -197, 148), new Vector3(118, -197, 148), new Vector3(180, -200, 133), new Vector3(70, -218, 40), new Vector3(147, -218, 40), new Vector3(217, -218, 40) };
    
    public static Vector3[] left_role_shore = new Vector3[] { new Vector3(-350, -197, 148), new Vector3(-450, -197, 148), new Vector3(-550, -200, 100), new Vector3(-370, -218, 40), new Vector3(-447, -218, 40), new Vector3(-557, -218, 40) };

    //角色相对于船的位置(相对坐标)
    public static Vector3[] role_boat = new Vector3[] { new Vector3(0, 6, 1), new Vector3(-1, 6, -3) };


}
