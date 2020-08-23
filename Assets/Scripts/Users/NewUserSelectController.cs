using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUserSelectController : MonoBehaviour
{
    public void OnMouseEnter()
    {
        var userController = gameObject.GetComponentInParent<UserController>();
        if(userController != null) {
            userController.MouseEnter();
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
