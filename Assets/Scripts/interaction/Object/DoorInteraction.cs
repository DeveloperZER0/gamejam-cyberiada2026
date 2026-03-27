using UnityEngine;

public class DoorInteraction : BaseInteraction
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private string targetSceneId;

    public override void Interact()
    {
        if (sceneSwitcher == null)
        {
            sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        }

        if (sceneSwitcher == null)
        {
            Debug.LogWarning("DoorInteraction: SceneSwitcher not found.");
            return;
        }

        sceneSwitcher.FadeToPseudoScene(targetSceneId);
    }
}
