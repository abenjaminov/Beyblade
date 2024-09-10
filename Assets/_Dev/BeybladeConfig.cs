using Assets._Dev.Behaviours;
using UnityEngine;
using UnityEngine.UI;

public class BeybladeConfig : MonoBehaviour
{
    
    public GameObject Skin;
	public GameObject Character;
    public FloatVariable MaxHealth;
    public float CurrentHealth;

    [Header("GUI")]
    public Slider _slider;

    [Header("Physics")]
    public float roundsPerMinute = 50f;

    GameObject skinInstance;

    private Spin Spin;

    public void Start()
    {
        skinInstance = Instantiate(Skin, transform);
        Spin = skinInstance.AddComponent<Spin>();

        Instantiate(Character, transform);

        CurrentHealth = MaxHealth.Value;
        _slider.value = 1;
    }

    public void ChangeHealth(float diff)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + diff,0, MaxHealth.Value);

        _slider.value = CurrentHealth / MaxHealth;
        this.roundsPerMinute = 100 + 5 * CurrentHealth;
    }

    private void Update()
    {
        Spin.roundsPerMinute = roundsPerMinute;   
    }
}