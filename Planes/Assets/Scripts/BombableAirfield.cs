using UnityEngine;

public class BombableAirfield : MonoBehaviour, IBombable
{
    public float WeightToKillPlane;
    public void takeBombDamage(float bombWeight)
    {
        Debug.Log("Ow!");
        int numHits = (int) Mathf.Floor(bombWeight/WeightToKillPlane);
        PlaneInventory inventory = GetComponent<PlaneInventory>();
        for(int i = 0; i < Mathf.Min(numHits,inventory.getPlanes().Count); i++)
        {
            int indToKill = Random.Range(0, inventory.getPlanes().Count);
            inventory.removePlane(inventory.getPlanes()[indToKill]);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
