using UnityEngine;

public class ChangeRoomInteraction : BaseInteraction
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private string targetSceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact() {
        sceneSwitcher.FadeToScene(targetSceneName);
    }
}
