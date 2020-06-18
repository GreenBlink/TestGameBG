using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveIndexColor(int index)
    {
        PlayerPrefs.SetInt("IndexColor", index);
    }

    public int GetIndexColor()
    {
        return PlayerPrefs.GetInt("IndexColor", 0);
    }

    public void SaveRecord(float value)
    {
        PlayerPrefs.SetFloat("Record", value);
    }

    public float GetRecord()
    {
        return PlayerPrefs.GetFloat("Record", 0);
    }
}
