
using UnityEngine;

using CharacterProperties;

public class GameManager: MonoSingleton<GameManager>
{
    public CharacterSetUp characterSetUp;
    public Character character;

    protected override void Init()
    {
        if (characterSetUp == null)
        {
            Debug.LogError("No Set Up File");
            return;
        }

        character = new Character(characterSetUp);
    }
}
    
