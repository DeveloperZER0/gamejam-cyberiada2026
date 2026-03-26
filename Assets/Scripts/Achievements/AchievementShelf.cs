using UnityEngine;

public class AchievementShelf : MonoBehaviour
{
    [SerializeField] private int maxShelfSlots = 3;
    [SerializeField] private int[] bitMask;
    [SerializeField] private GameObject[] shelfSlots;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          // unhide shelfslots based on bitMask
          for (int i = 0; i < maxShelfSlots; i++) {
              if (bitMask[i] == 1) {
                  shelfSlots[i].SetActive(true);
              }
          }
    }

    public void SetBitMask(int index) {
        if (index >= 0 && index < maxShelfSlots) {
            bitMask[index] = 1;
        }
    }

    public int[] GetBitMask() {
        return bitMask;
    }
}
