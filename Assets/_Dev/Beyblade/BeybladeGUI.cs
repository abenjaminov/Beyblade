using UnityEngine;
using UnityEngine.UI;

public class BeybladeGUI: MonoBehaviour
{
    BeybladeLiveConfiguration config;

    public Slider _slider;

    public void Awake()
    {
        config = GetComponent<BeybladeLiveConfiguration>();
        config.CurrentHealth.OnValueChangeEvent += OnCurrentHealthChanged;
    }

    private void OnCurrentHealthChanged()
    {
        _slider.value = config.CurrentHealth / config.MaxHealth;
    }
}