using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    ShoreCtrl leftShoreController, rightShoreController;
    River river;
    BoatCtrl boatController;
    RoleCtrl[] roleControllers = new RoleCtrl[6];
    MoveCtrl moveController;
    public bool isRunning;
    float time;

    public void LoadResources()
    {
        //如果有，则释放原有的GameObject
        for (int i = 0; i < 6; i++)
        {
            if (roleControllers[i] != null)
            {
                Destroy(roleControllers[i].GetModelGameObject());
            }
        }
        if (leftShoreController != null) Destroy(leftShoreController.GetModelGameObject());
        if (rightShoreController != null) Destroy(rightShoreController.GetModelGameObject());
        if (boatController != null)
        {
            Destroy(boatController.GetModelGameObject());
        }

        //role
        roleControllers = new RoleCtrl[6];
        for (int i = 0; i < 6; ++i)
        {
            roleControllers[i] = new RoleCtrl();
            roleControllers[i].CreateRole(Position.right_role_shore[i], i < 3 ? true : false, i);
        }

        //shore
        leftShoreController = new ShoreCtrl();
        leftShoreController.CreateShore(Position.left_shore,false);
        leftShoreController.GetShore().shore.name = "left_shore";
        rightShoreController = new ShoreCtrl();
        rightShoreController.CreateShore(Position.right_shore,true);
        rightShoreController.GetShore().shore.name = "right_shore";

        //将人物添加并定位至右
        foreach (RoleCtrl roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition = rightShoreController.AddRole(roleController.GetRoleModel());
        }
        //boat
        boatController = new BoatCtrl();
        boatController.CreateBoat(Position.right_boat);

        //river
        if (river == null)
            river = new River(Position.river);

        //move
        moveController = new MoveCtrl();

        isRunning = true;
        time = 90;
    }

    void Awake()
    {
        SSDirector.GetInstance().CurrentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
    }

    void Update()
    {
        if (isRunning)
        {
            time -= Time.deltaTime;
            this.gameObject.GetComponent<UserGUI>().time = (int)time;
            if (time <= 0)
            {
                this.gameObject.GetComponent<UserGUI>().time = 0;
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRunning = false;
            }
        }
    }

    public void RestartGame()
    {
        LoadResources();
    }


    public void MoveBoat()
    {
        if (isRunning == false || moveController.GetIsMoving()) return;
        if (boatController.GetBoatModel().isLeft)
        {
            moveController.SetMove(Position.right_boat, boatController.GetBoatModel().boat);
        }
        else
        {
            moveController.SetMove(Position.left_boat, boatController.GetBoatModel().boat);
        }
        boatController.GetBoatModel().isLeft = !boatController.GetBoatModel().isLeft;
    }

    public void MoveRole(Role roleModel)
    {
        if (isRunning == false || moveController.GetIsMoving())
        {
            //Debug.Log("isRunning == false || moveController.GetIsMoving() role clicked RETURN");
            return;
        }
        if (roleModel.inBoat) //在船上，再点返回原来所在的shore
        {
            //Debug.Log("In boat role clicked!! priest count:" + rightShoreController.GetShore().priestCount + "devil count:" + rightShoreController.GetShore().devilCount);
            if (boatController.GetBoatModel().isLeft)
            {
                moveController.SetMove(leftShoreController.AddRole(roleModel), roleModel.role);
                roleModel.role.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                moveController.SetMove(rightShoreController.AddRole(roleModel), roleModel.role);
                roleModel.role.transform.eulerAngles = new Vector3(0, -115, 0);
            }
            roleModel.onLeft = boatController.GetBoatModel().isLeft;
            boatController.RemoveRole(roleModel);
            //Debug.Log("In boat role clicked!! priest count:" + rightShoreController.GetShore().priestCount + "devil count:" + rightShoreController.GetShore().devilCount);
        }
        else //在岸上
        {
            if (!boatController.HasPosition())
            {
                //Debug.Log("!boatController.HasPosition() role clicked RETURN");
                return;
            }
            if (boatController.GetBoatModel().isLeft == roleModel.onLeft) //船和人在一边
            {
                //Debug.Log("On shore role clicked!! priest count:"+rightShoreController.GetShore().priestCount+  ";devil count:" +rightShoreController.GetShore().devilCount);
                if (roleModel.onLeft)
                {
                    leftShoreController.RemoveRole(roleModel);
                }
                else
                {
                    rightShoreController.RemoveRole(roleModel);
                }
                moveController.SetMove(boatController.AddRole(roleModel), roleModel.role);
                //Debug.Log("On shore role clicked fin!! priest count:" + rightShoreController.GetShore().priestCount + ";devil count:" + rightShoreController.GetShore().devilCount);
            }
        }
    }

    public void Check()
    {
        if (isRunning == false) return;
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        if (leftShoreController.GetShore().priestCount == 3)
        {
            this.gameObject.GetComponent<UserGUI>().gameMessage = "You Win!";
            isRunning = false;
        }
        else
        {
            int leftPriestCount, rightPriestCount, leftDevilCount, rightDevilCount;
            leftPriestCount = leftShoreController.GetShore().priestCount + (boatController.GetBoatModel().isLeft ? boatController.GetBoatModel().priestCount : 0);
            rightPriestCount = rightShoreController.GetShore().priestCount + (boatController.GetBoatModel().isLeft ? 0 : boatController.GetBoatModel().priestCount);
            leftDevilCount = leftShoreController.GetShore().devilCount + (boatController.GetBoatModel().isLeft ? boatController.GetBoatModel().devilCount : 0);
            rightDevilCount = rightShoreController.GetShore().devilCount + (boatController.GetBoatModel().isLeft ? 0 : boatController.GetBoatModel().devilCount);
            //Debug.Log("LEFT PREST COUNT:" + leftPriestCount);
            //Debug.Log("RIGHT PREST COUNT:" + rightPriestCount);
            //Debug.Log("LEFT DEVIL COUNT:" + leftPriestCount);
            //Debug.Log("RIGHT DEVIL COUNT:" + rightPriestCount);
            if (((rightPriestCount != 0 && rightPriestCount < rightDevilCount) || (leftPriestCount != 0 && leftPriestCount < leftDevilCount)) && moveController.GetIsMoving() == false)
            {
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRunning = false;
            }
        }
    }
}