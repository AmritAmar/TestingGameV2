using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAttributes: MonoBehaviour
{
    [Header("Land Properties")]     
    public string lname;
    public int ID;
    public bool isResource;

    //If Natural Resource
    public int maxAmount;
    public int curAmount;

    public Material baseMat;

    public void setAttributes(string _name, int _ID, bool _isResource, int _maxAmount, Material _baseColor) {
        lname = _name;
        ID = _ID;
        isResource = _isResource;
        maxAmount = _maxAmount;
        curAmount = _maxAmount;
        baseMat = _baseColor;
    }

    void Start()
    {
    }

}
