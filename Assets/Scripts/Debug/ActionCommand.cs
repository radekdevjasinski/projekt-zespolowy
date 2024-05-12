using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : CommandBase
{
    private Func<string> _func;

    public ActionCommand(string id, string description, string usage, Func<string> func) : base(id, description, usage)
    {
        _func = func;
    }

    public string invoke()
    {
        return _func.Invoke(); 
    }

}
