using UnityEngine;

public class NewUserSelectController : MonoBehaviour
{
    public void OnMouseEnter()
    {
        var userController = gameObject.GetComponentInParent<UserController>();
        if(userController != null) 
        {
            userController.MouseEnter();
        }
    }

    public void OnMouseExit()
    {
        var userController = gameObject.GetComponentInParent<UserController>();
        if(userController != null) 
        {
            userController.MouseExit();
        }
    }
}
