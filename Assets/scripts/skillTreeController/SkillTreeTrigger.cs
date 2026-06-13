using UnityEngine;

public class SkillTreeTrigger : MonoBehaviour
{
    public GameObject skillTreeUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            skillTreeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("neUSOOO");
        
        if (other.CompareTag("Player"))
        {

            Debug.Log("USOOO");
            skillTreeUI.SetActive(false);
        }
    }
}