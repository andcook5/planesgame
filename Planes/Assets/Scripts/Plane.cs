using UnityEngine;
[System.Serializable]
public class Plane
{
    public float speed;
    public float bombCapacity;
    public string typeName;
    public float bombAccuracy;
    public bool UIBoxChecked;
    public Plane(float speed, float bombCapacity, string typeName, float bombAccuracy)
    {
        this.speed = speed;
        this.bombCapacity = bombCapacity;
        this.typeName = typeName;
        this.bombAccuracy = bombAccuracy;
    }
}
