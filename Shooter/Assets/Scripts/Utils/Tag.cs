using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Tag system for identifying objects
/// </summary>
[DisallowMultipleComponent]
public class Tag : MonoBehaviour
{
    [SerializeField]
    public TagID[] tagIDs = new TagID[0];

    public void AddTagID(TagID tagID)
    {
        if (!HasTagID(tagID))
        {
            List<TagID> l = new List<TagID>(tagIDs);
            l.Add(tagID);
            tagIDs = l.ToArray();
        }
    }

    public bool HasTagID(TagID tagID)
    {
        bool found = false;
        for (int i = 0; i < tagIDs.Length; i++)
        {
            if (tagIDs[i] == tagID)
            {
                found = true;
                break;
            }
        }
        return found;
    }

    public bool HasAnyOfTagIDs(TagID[] lookTagIds)
    {
        bool found = false;
        for (int i = 0; i < lookTagIds.Length; i++)
        {
            if (HasTagID(lookTagIds[i]))
            {
                found = true;
                break;
            }
        }
        return found;
    }

    static public GameObject[] FindTaggedObjects(TagID[] tagsIds)
    {
        Tag[] allTaggedObjects = GameObject.FindObjectsOfType<Tag>();
        List<GameObject> resultSet = new List<GameObject>();
        for (int i = 0; i < allTaggedObjects.Length; i++)
        {
            if (allTaggedObjects[i].HasAnyOfTagIDs(tagsIds) && !resultSet.Contains(allTaggedObjects[i].gameObject))
            {
                resultSet.Add(allTaggedObjects[i].gameObject);
            }
        }
        return resultSet.ToArray();
    }

}