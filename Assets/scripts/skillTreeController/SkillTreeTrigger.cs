using UnityEngine;

public class SkillTreeTrigger : MonoBehaviour
{
    public SkillTreeUIController ui;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ui.Open();
        }
    }
}