using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Classes characterClass;
    private Character character;
    private int maxHealth;
    private int health;

    public Character getCharacter() { return this.character; }

    private void Start()
    {
        if (characterClass == Classes.Knight)
        {
            character = (Knight) gameObject.AddComponent(typeof(Knight));
        } else if (characterClass == Classes.Mage)
        {
            character = (Mage) gameObject.AddComponent(typeof(Mage));
        } else if (characterClass == Classes.Ninja)
        {
            character = (Ninja) gameObject.AddComponent(typeof(Ninja));
        }
    }

}
