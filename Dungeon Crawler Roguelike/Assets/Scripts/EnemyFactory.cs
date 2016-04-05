using UnityEngine;

public class EnemyFactory : MonoBehaviour, MovingObjectFactory
{
    public GameObject GetObject(string identifier)
    {
        switch(identifier)
        {
            case "Beholder":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Beholder")) as GameObject);
            case "DarkElf":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/DarkElf")) as GameObject);
            case "Drake":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Drake")) as GameObject);
            case "LizardWarrior":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/LizardWarrior")) as GameObject);
            case "Minotaur":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Minotaur")) as GameObject);
            case "Naga":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Naga")) as GameObject);
            case "Skeleton":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Skeleton")) as GameObject);
            case "Troll":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Troll")) as GameObject);
            case "Witch":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Witch")) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetObject(string identifier, Vector3 position, Quaternion rotation)
    {
        switch (identifier)
        {
            case "Beholder":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Beholder"), position, rotation) as GameObject);
            case "DarkElf":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/DarkElf"), position, rotation) as GameObject);
            case "Drake":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Drake"), position, rotation) as GameObject);
            case "LizardWarrior":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/LizardWarrior"), position, rotation) as GameObject);
            case "Minotaur":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Minotaur"), position, rotation) as GameObject);
            case "Naga":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Naga"), position, rotation) as GameObject);
            case "Skeleton":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Skeleton"), position, rotation) as GameObject);
            case "Troll":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Troll"), position, rotation) as GameObject);
            case "Witch":
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Witch"), position, rotation) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetRandomObject()
    {
        switch (Random.Range(1, 9))
        {
            case 1:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Beholder")) as GameObject);
            case 2:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/DarkElf")) as GameObject);
            case 3:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Drake")) as GameObject);
            case 4:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/LizardWarrior")) as GameObject);
            case 5:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Minotaur")) as GameObject);
            case 6:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Naga")) as GameObject);
            case 7:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Skeleton")) as GameObject);
            case 8:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Troll")) as GameObject);
            case 9:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Witch")) as GameObject);
            default:
                return null;
        }
    }

    public GameObject GetRandomObject(Vector3 position, Quaternion rotation)
    {
        switch (Random.Range(1, 9))
        {
            case 1:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Beholder"), position, rotation) as GameObject);
            case 2:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/DarkElf"), position, rotation) as GameObject);
            case 3:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Drake"), position, rotation) as GameObject);
            case 4:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/LizardWarrior"), position, rotation) as GameObject);
            case 5:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Minotaur"), position, rotation) as GameObject);
            case 6:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Naga"), position, rotation) as GameObject);
            case 7:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Skeleton"), position, rotation) as GameObject);
            case 8:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Troll"), position, rotation) as GameObject);
            case 9:
                return (GameObject)(Instantiate(Resources.Load("Prefabs/Enemies/Witch"), position, rotation) as GameObject);
            default:
                return null;
        }
    }
}
