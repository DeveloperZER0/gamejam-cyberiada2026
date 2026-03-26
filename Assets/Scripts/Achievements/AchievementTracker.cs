using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    [SerializeField] private AchievementShelf achievementShelf;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // load bitMask from PlayerPrefs and set it to achievementShelf bitMask
        // if no bitMask is found initialize it to an array of 0set
        if (PlayerPrefs.HasKey("achievementBitMask")) {
            string bitMaskString = PlayerPrefs.GetString("achievementBitMask");
            string[] bitMaskArray = bitMaskString.Split(',');
            int[] bitMask = new int[bitMaskArray.Length];
            for (int i = 0; i < bitMaskArray.Length; i++) {
              bitMask[i] = int.Parse(bitMaskArray[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // on clicking item marked as achievement set achievementShelf bitMask to 1 for that item
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.CompareTag("AchievementItem")) {
                    int itemIndex = hit.collider.GetComponent<AchievementItem>().itemIndex;
                    achievementShelf.SetBitMask(itemIndex);
                    saveAchievementBitMask();
                }
            }
        }
        // serialize the bitMask to PlayerPrefs so that it can be loaded on next playthrough
        
    }

    private void saveAchievementBitMask() {
        string bitMaskString = string.Join(",", achievementShelf.GetBitMask());
        PlayerPrefs.SetString("achievementBitMask", bitMaskString);
    }
}
