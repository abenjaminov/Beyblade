using Assets._Dev.Behaviours;
using UnityEngine;

public class BeybladeConfig : MonoBehaviour
{
	public GameObject Skin;
	public GameObject Character;

    GameObject skinInstance;

    public void Start()
    {
        skinInstance = Instantiate(Skin, transform);
        skinInstance.AddComponent<Spin>().rotationSpeed = 1000f;

        Instantiate(Character, transform);
    }
}