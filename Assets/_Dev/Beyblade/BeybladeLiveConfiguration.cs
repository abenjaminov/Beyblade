using UnityEngine;

public class BeybladeLiveConfiguration: MonoBehaviour, IBeybladeConfiguration<FloatVar>
{
    [SerializeField] BeybladeConfiguration config;

    public GameObject Character
    {
        get
        {
            return config.Character;
        }
    }

    public GameObject Skin
    {
        get
        {
            return config.Skin;
        }
    }

    public FloatVar maxHealth;
    public FloatVar currentHealth;
    public FloatVar attack;
    public FloatVar defense;
    public FloatVar roundsPerMinute;

    public FloatVar MaxHealth 
    { 
        get 
        {
            return maxHealth;
        } 
        set 
        {
            maxHealth = value;
        } 
    }

    public FloatVar Attack
    {
        get
        {
            return attack;
        }
        set
        {
            attack = value;
        }
    }

    public FloatVar Defense
    {
        get
        {
            return defense;
        }
        set
        {
            defense = value;
        }
    }

    public FloatVar CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public FloatVar RoundsPerMinute
    {
        get
        {
            return roundsPerMinute;
        }
        set
        {
            roundsPerMinute = value;
        }
    }

    void Awake()
    {
        Attack = new FloatVar(config.Attack.Value);
        Defense = new FloatVar(config.Defense);
        CurrentHealth = new FloatVar(config.CurrentHealth);
        RoundsPerMinute = new FloatVar(config.RoundsPerMinute);
        MaxHealth = new FloatVar(config.MaxHealth);
    }
}