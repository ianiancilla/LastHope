using UnityEngine;
using UnityEngine.UI;

public class UI_SectorEvacGauge : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Sector mySector;
    [SerializeField] private Image evacGaugeImg;

    // Update is called once per frame
    void Update()
    {
        evacGaugeImg.fillAmount = mySector.GetEvacState();
    }
}
