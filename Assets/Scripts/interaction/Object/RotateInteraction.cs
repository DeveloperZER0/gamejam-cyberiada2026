using UnityEngine;

public class RotateInteraction : BaseInteraction
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isRotated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact() {
        isRotated = !isRotated;
        animator.SetBool("isRotated", isRotated);
    }
}
