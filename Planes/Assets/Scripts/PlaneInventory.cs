using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Unity.Properties;

public class PlaneInventory : MonoBehaviour, ISelectable
{
    public UIDocument UIDoc;
    [SerializeField]
    private List<Plane> planeList = new();
    private Dictionary<string, int> typesDict = new Dictionary<string, int>();

    public int numPlanes = 0;
    void Start()
    {
        addPlane(new Plane(0, 0, "peepee-1",1));
    }
    private void addToTypesDict(Plane plane)
    {
        string typeName = plane.typeName;
        if (typesDict.ContainsKey(typeName))
        {
            typesDict[typeName] += 1;
        }
        else
        {
            typesDict.Add(typeName, 1);
        }
    }
    public void addPlane(Plane plane)
    {
        planeList.Add(plane);
        addToTypesDict(plane);
        numPlanes++;
    }
    public void removePlane(Plane plane)
    {
        if (planeList.Contains(plane))
        {
            planeList.Remove(plane);
            numPlanes--;
            typesDict[plane.typeName]--;
            if (typesDict[plane.typeName] == 0)
            {
                typesDict.Remove(plane.typeName);
            }
        }
    }
    public List<Plane> getPlanes()
    {
        return planeList;
    }
    public void updateUI()
    {
        VisualElement root = UIDoc.rootVisualElement;
        VisualElement listView = root.Q<ListView>();
        listView.dataSource = this;
        listView.SetBinding("itemsSource", new DataBinding() { dataSourcePath = new PropertyPath("planeList") });
    }

    void ISelectable.OnSelected()
    {
        UIDoc.enabled = true;
        updateUI();
    }

    void ISelectable.OnDeselected()
    {
        UIDoc.enabled = false;
    }

    bool ISelectable.IsSelected()
    {
        return UIDoc.enabled;
    }
    void transferInventoryTo(PlaneInventory target)
    {
        foreach(Plane p in planeList)
        {
            target.addPlane(p);
            this.removePlane(p);
        }
    }
}
