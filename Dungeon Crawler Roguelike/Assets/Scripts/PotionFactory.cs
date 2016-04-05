using UnityEngine;

public class PotionFactory : MonoBehaviour
{
    public static GameObject GetRandomPotion()
    {
        GameObject potion;
        switch (Random.Range(1, 6))
        {
            case 1:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthLarge")) as GameObject);
                potion.AddComponent<LargePotion>().SetPotion(new Potion());
                return potion;
            case 2:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthMedium")) as GameObject);
                potion.AddComponent<MediumPotion>().SetPotion(new Potion());
                return potion;
            case 3:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthSmall")) as GameObject);
                potion.AddComponent<Potion>();
                return potion;
            case 4:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaLarge")) as GameObject);
                potion.AddComponent<LargePotion>().SetPotion(new Potion());
                return potion;
            case 5:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaMedium")) as GameObject);
                potion.AddComponent<MediumPotion>().SetPotion(new Potion());
                return potion;
            case 6:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaSmall")) as GameObject);
                potion.AddComponent<Potion>();
                return potion;
            default:
                return null;
        }
    }
    public static GameObject GetRandomPotion(Vector3 position, Quaternion rotation)
    {

        GameObject potion;
        switch (Random.Range(1, 6))
        {
            case 1:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthLarge"), position, rotation) as GameObject);
                potion.AddComponent<LargePotion>().SetPotion(new Potion());
                return potion;
            case 2:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthMedium"), position, rotation) as GameObject);
                potion.AddComponent<MediumPotion>().SetPotion(new Potion());
                return potion;
            case 3:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/HealthSmall"), position, rotation) as GameObject);
                potion.AddComponent<Potion>();
                return potion;
            case 4:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaLarge"), position, rotation) as GameObject);
                potion.AddComponent<LargePotion>().SetPotion(new Potion());
                return potion;
            case 5:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaMedium"), position, rotation) as GameObject);
                potion.AddComponent<MediumPotion>().SetPotion(new Potion());
                return potion;
            case 6:
                potion = (GameObject)(Instantiate(Resources.Load("Prefabs/Items/Potions/ManaSmall"), position, rotation) as GameObject);
                potion.AddComponent<Potion>();
                return potion;
            default:
                return null;
        }
    }
}
