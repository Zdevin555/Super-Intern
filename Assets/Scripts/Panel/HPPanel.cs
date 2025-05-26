using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPPanel : MonoBehaviour
{
    public Slider HPSlider;
    public TextMeshProUGUI LifeText;

    private void Start()
    {

    }

    public void SetHP(float ratio)
    {
        if (HPSlider != null)
        {
            HPSlider.value = ratio;
        }
    }

    public void SetLife(int life)
    {
        if (LifeText != null)
        {
            LifeText.text = life.ToString();
        }
    }

}
