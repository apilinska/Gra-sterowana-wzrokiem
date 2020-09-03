using UnityEngine;
using UnityEngine.EventSystems;

public class CursorCollider : MonoBehaviour
{
    private bool onCollision = false;

    public bool onCollisionWithCursor() 
    {
        return this.onCollision;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.tag == "cursor") 
        {
            this.onCollision = true;
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerEnterHandler);
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        if(col.gameObject.tag == "cursor" && this.onCollision) 
        {
            this.onCollision = false;
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerExitHandler);
        }
    }
}
