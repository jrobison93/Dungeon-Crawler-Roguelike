using UnityEngine;

public interface MovingObjectFactory
{
    GameObject GetObject(string identifier);
    GameObject GetObject(string identifier, Vector3 position, Quaternion rotation);
    GameObject GetRandomObject();
    GameObject GetRandomObject(Vector3 position, Quaternion rotation);
}
