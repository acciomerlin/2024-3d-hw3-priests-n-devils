using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoreCtrl
{
    Shore shoreModel;

    public void CreateShore(Vector3 position,bool isRight)
    {
        if (shoreModel == null)
        {
            shoreModel = new Shore(position,isRight);
        }
    }

    /**
     *GetShore()：提供整个 Shore 对象，供需要全面访问 Shore 功能的代码使用。
	 *GetModelGameObject()：仅返回 shoreModel 的 GameObject 部分，便于场景中对 GameObject 进行位置调整、渲染、或物理处理等操作。
    **/
    public Shore GetShore()
    {
        return shoreModel;
    }

    public GameObject GetModelGameObject()
    {
        return shoreModel.shore;
    }

    //将角色添加到岸上，返回角色在岸上的相对坐标
    public Vector3 AddRole(Role roleModel)
    {
        roleModel.role.transform.parent = shoreModel.shore.transform;
        roleModel.inBoat = false;
        if (roleModel.isPriest) shoreModel.priestCount++;
        else shoreModel.devilCount++;
        if(shoreModel.isRight) return Position.right_role_shore[roleModel.id];
        else return Position.left_role_shore[roleModel.id];
    }

    //将角色从岸上移除
    public void RemoveRole(Role roleModel)
    {
        if (roleModel.isPriest) shoreModel.priestCount--;
        else shoreModel.devilCount--;
    }

}