using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpened = false;
    private Animator anim;

    // Delegate and event to notify when the chest is opened
    public delegate void ChestOpenedAction();
    public event ChestOpenedAction OnChestOpened;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isOpened)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;

            // Play the chest opening animation
            anim.Play("ChestOpen");

            // Notify the LevelManager that this chest has been opened
            OnChestOpened?.Invoke();

            // Additional chest opening logic (e.g., give rewards) can go here
        }
    }
}
