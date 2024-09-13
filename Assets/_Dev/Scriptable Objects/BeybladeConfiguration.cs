using UnityEngine;

[CreateAssetMenu(fileName = "Beyblade Config", menuName = "Beyblades/Config", order = 1)]
public class BeybladeConfiguration: ScriptableObject
{
    public GameObject Skin;
    public GameObject Character;
    public FloatVariable MaxHealth;
    public FloatVariable Attack;
    public FloatVariable Defense;
    public FloatVariable CurrentHealth;
    public FloatVariable RoundsPerMinute;
}