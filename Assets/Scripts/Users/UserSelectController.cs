using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSelectController : MonoBehaviour
{
    public void OnMouseEnter()
    {
        string name = gameObject.GetComponentInChildren<Text>().text;
        var userController = gameObject.GetComponentInParent<UserController>();
        if(userController != null) {
            userController.MouseEnter(name);
        }
    }

    public void OnMouseExit()
    {
        var userController = gameObject.GetComponentInParent<UserController>();
        if(userController != null) {
            userController.MouseExit();
        }
    }
}
