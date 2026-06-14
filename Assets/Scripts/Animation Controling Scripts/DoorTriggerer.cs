using UnityEngine;

public class DoorTriggerer : MonoBehaviour
{

    private bool flag = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            transform.GetComponent<Animator>().SetBool("IsDoorTriggered", true);
            flag = false;
        }
        
        
    }
}
