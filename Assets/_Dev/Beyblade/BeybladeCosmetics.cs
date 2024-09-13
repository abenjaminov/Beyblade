using Assets._Dev.Behaviours;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BeybladeCosmetics: MonoBehaviour
{
    BeybladeLiveConfiguration config;

    GameObject skinInstance;

    void Awake()
    {
        config = GetComponent<BeybladeLiveConfiguration>();

        Instantiate(config.Character, transform);
        skinInstance = Instantiate(config.Skin, transform);
        skinInstance.AddComponent<Spin>();
    }
}