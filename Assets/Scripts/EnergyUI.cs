using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public EnergySystem energy;
    public Image fillImage;
    [SerializeField] TMP_Text text;

    void Update()
    {
        float value = energy.currentEnergy / energy.maxEnergy;
        fillImage.fillAmount = value;
        int enrg = (int)energy.GetEnergy();
        text.text = enrg.ToString();
    }
}