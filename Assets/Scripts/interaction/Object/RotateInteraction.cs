using UnityEngine;

public class RotateInteraction : BaseInteraction
{
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact() {
      // Toggle inspect state (zoom in on the object)
      // If in inspect state, rotate the object, otherwise reset rotation 
      // Use the same fade in and out animation as the open interaction 
      // On click outside of main collider, exit inspect state and reset rotation
      FadeInAndOut(animator, "isRotated");
      /*if(!isRotated) {
        animator.SetBool("isRotated", true);
        isRotated = true;
      } else {
        animator.SetBool("isRotated", false);
        isRotated = false;
      }*/
    }
}
