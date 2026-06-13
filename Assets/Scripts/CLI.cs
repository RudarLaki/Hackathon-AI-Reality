using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class HologramTerminal : MonoBehaviour
{
    [SerializeField] private GameObject hologram;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text errorText;
    
    [SerializeField] Material mat;
    [SerializeField] float intensity = 2f;
    
    [SerializeField] float duration = 5f;
    
    [SerializeField] float slowAmount = 0.2f;
    
    [SerializeField] float lowGravity = -2f;
    [SerializeField] float normalGravity = -9.81f;
    
    [SerializeField] float blinkDistance = 6f;
    [SerializeField] float maxWallThickness = 1f;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] Transform player;
    
    [SerializeField] float invisibleAlpha = 0.2f;
    
    [SerializeField] float malfunctionChance;
    
    [SerializeField] Material playerMat;

    [SerializeField] private EnergySystem energy;
    [SerializeField] private PlayerControler PC;
    
    
    private bool isActive = false;
    private string currentText = "";
    private Dictionary<string, BugCommand> commands;
    

    void Start()
    {
        Physics.gravity = new Vector3(0, normalGravity, 0);
        
        
        commands = new Dictionary<string, BugCommand>()
        {
            {"/dodge", new BugCommand(ActivateDodge, 1000)},
            {"/speed", new BugCommand(ActivateSpeed, 20)},
            {"/slow", new BugCommand(ActivateSlow, 30)},
            {"/lowg", new BugCommand(ActivateLowGravity, 25)},
            {"/blink", new BugCommand(ActivateBlink, 15)},
            {"/invis", new BugCommand(ActivateInvisibility, 40)},
            {"/split", new BugCommand(ActivateSplit, 50)}
        };
    }
    void Update()
    {
        float currEnrg = energy.GetEnergy();
        if (currEnrg < 30) malfunctionChance = 0.5f;
        else if (currEnrg <= 80) malfunctionChance = 0.3f;
        else malfunctionChance = 0;
        
        float flicker = 0.8f + Mathf.PerlinNoise(Time.time * 5f, 0f) * 0.4f;
        Color col = Color.darkGreen * flicker * intensity;
        mat.SetColor("_BaseColor", col);

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            isActive = !isActive;
            hologram.SetActive(isActive);

            if (isActive)
                currentText = "";

            text.text = currentText;
        }

        if (!isActive)
            return;
        
        HandleTyping();
        foreach (var command in commands)
        {
            if (currentText.EndsWith(command.Key, StringComparison.OrdinalIgnoreCase))
            {
                ExecuteBug(command.Value);
                break;
            }
        }
    }
    void ExecuteBug(BugCommand bug)
    {
        if (!energy.UseEnergy(bug.energyCost))
        {
            currentText = "";
            text.text = currentText;
            errorText.text = "ERROR\nERROR\nERROR";
            StartCoroutine(Error());
            Debug.Log("Not enough energy!");
            return;
        }
        
        isActive = !isActive;
        hologram.SetActive(isActive);

        if (Random.value < malfunctionChance)
        {
            Malfunction();
        }

        bug.action.Invoke();
    }
    System.Collections.IEnumerator Error(){
        yield return new WaitForSecondsRealtime(1);
        errorText.text = "";
    }

    void Malfunction()
    {
        Debug.Log("KUR");
        int bug = Random.Range(0, 6);
        switch (bug)
        {
            case 0:
                ActivateDodge();
                break;
            case 1:
                ActivateSpeed();
                break;
            case 2:
                ActivateSlow();
                break;
            case 3:
                ActivateLowGravity();
                break;
            case 4:
                ActivateBlink();
                break;
            case 5:
                ActivateInvisibility();
                break;
        }
    }

    void HandleTyping()
    {
        foreach (char c in Input.inputString)
        {
            if (c == 't' && Keyboard.current.tKey.wasPressedThisFrame)
                continue;
            if (c == '\b')
            {
                if (currentText.Length > 0)
                    currentText = currentText[..^1];
            }
            else if (c == '\n' || c == '\r')
            {
                currentText += "\n";
            }
            else
            {
                currentText += c;
            }

            text.text = currentText;
        }
    }
    void ActivateDodge()
    {
        Debug.Log("Dodge unlocked!");
    }

    

    void ActivateSpeed()
    {
        StartCoroutine(Speed());
        Debug.Log("Speed unlocked!");
    }

    System.Collections.IEnumerator Speed()
    {
        PC.moveSpeed *= 1.5f;
        yield return new WaitForSecondsRealtime(duration);    
        PC.moveSpeed /= 1.5f;
    }


    void ActivateSlow()
    {
        Debug.Log("Slow unlocked!");
        StartCoroutine(Slow());
    }
    
    System.Collections.IEnumerator Slow()
    {
        Time.timeScale = slowAmount;

        yield return new WaitForSecondsRealtime(duration);
        Debug.Log("Slow ended!");

        Time.timeScale = 1f;
    }

    void ActivateLowGravity()
    {
        Debug.Log("Low gravity unlocked!");    
        StartCoroutine(LowGravity());
    }

    System.Collections.IEnumerator LowGravity()
    {
        Physics.gravity = new Vector3(0, lowGravity, 0); 
        yield return new WaitForSecondsRealtime(duration);
        Physics.gravity = new Vector3(0, normalGravity, 0);
        Debug.Log("Low gravity ended!");  
    }

    void ActivateBlink()
    {
        if (player == null) return;

        Vector3 start = player.position;
        Vector3 dir = player.forward;

        if (Physics.Raycast(start, dir, out RaycastHit hit, blinkDistance, obstacleMask))
        {
            Vector3 hitPoint = hit.point;

            if (Physics.Raycast(hitPoint + dir * 0.1f, -dir, out RaycastHit backHit, maxWallThickness, obstacleMask))
            {
                // thick wall → stop blink
                player.position = hitPoint - dir * 0.5f;
                return;
            }

            // thin wall → go through
            player.position = start + dir * blinkDistance;
            return;
        }

        // no wall
        player.position = start + dir * blinkDistance;
    }

    void ActivateInvisibility()
    {
        Debug.Log("Invisibility unlocked!");    
        StartCoroutine(Invisibility());
    }

    System.Collections.IEnumerator Invisibility()
    {
        Color col = playerMat.color;
        col.a = invisibleAlpha;
        playerMat.color = col;
        
        yield return new WaitForSecondsRealtime(duration);

        col.a = 1;
        playerMat.color = col;
        Debug.Log("Invisibility ended!");    
    }

    void ActivateSplit()
    {
        Debug.Log("Split unlocked!");
    }
    
    
    public class BugCommand
    {
        public Action action;
        public float energyCost;

        public BugCommand(Action action, float energyCost)
        {
            this.action = action;
            this.energyCost = energyCost;
        }
    }
}