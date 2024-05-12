using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBase
{
    private string _id;
    private string _description;
    private string _usage;

    public CommandBase(string id, string description, string usage)
    {
        this._id = id;
        this._description = description;
        this._usage = usage;
    }

    public CommandBase(string id)
    {
        this._id = id;
    }

    public string id => _id;

    public string description => _description;

    public string usage => _usage;


    public override string ToString()
    {
        return id + " - " + description + "\n[ " + usage + " ]";
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj as CommandBase != null)
        {
            return (obj as CommandBase).id == this._id;
        }
        else
        {
            return false;
        }
    }
}
