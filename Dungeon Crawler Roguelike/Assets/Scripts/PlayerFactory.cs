using UnityEngine;

public class PlayerFactory : MonoBehaviour, MovingObjectFactory
{
    public GameObject GetObject(string identifier)
    {
        switch (identifier)
        {
            case "Mage":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Mage")) as GameObject);
            case "Paladin":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Paladin")) as GameObject);
            case "Ranger":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Ranger")) as GameObject);
            case "Rouge":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Rogue")) as GameObject);
            case "Warrior":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Warrior")) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetObject(string identifier, Vector3 position, Quaternion rotation)
    {
        switch (identifier)
        {
            case "Mage":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Mage"), position, rotation) as GameObject);
            case "Paladin":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Paladin"), position, rotation) as GameObject);
            case "Ranger":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Ranger"), position, rotation) as GameObject);
            case "Rouge":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Rogue"), position, rotation) as GameObject);
            case "Warrior":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Warrior"), position, rotation) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetRandomObject()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Mage")) as GameObject);
            case 2:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Paladin")) as GameObject);
            case 3:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Ranger")) as GameObject);
            case 4:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Rogue")) as GameObject);
            case 5:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Warrior")) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetRandomObject(Vector3 position, Quaternion rotation)
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Mage"), position, rotation) as GameObject);
            case 2:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Paladin"), position, rotation) as GameObject);
            case 3:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Ranger"), position, rotation) as GameObject);
            case 4:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Rogue"), position, rotation) as GameObject);
            case 5:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/PlayerClasses/Warrior"), position, rotation) as GameObject);
            default:
                return null;
        }
    }
}
