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

    GameObject skinInstance;

    public void Start()
    {
        skinInstance = Instantiate(Skin, transform);
        skinInstance.AddComponent<Spin>().rotationSpeed = 2500f;

        Instantiate(Character, transform);

        CurrentHealth = MaxHealth.Value;
        _slider.value = 1;
    }

    public void ChangeHealth(float diff)
    {
        CurrentHealth += diff;

        _slider.value = CurrentHealth / MaxHealth;
    }
}