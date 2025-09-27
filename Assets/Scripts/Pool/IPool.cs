using UnityEngine;

public interface IPool
{
    GameObject GetItem();
    void ReleaseItem(GameObject gameObject);
    void ReleaseAll();
}
