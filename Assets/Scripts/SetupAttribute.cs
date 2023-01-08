using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SetupAttribute
{
    public SetupAttribute(
        int id, 
        string group,
        string name, 
        string value, 
        string desc
    )
    {
        this.Id = id;
        this.Group = group;
        this.Name = name;
        this.Value = value;
        this.Desc = desc;
    }
    public int Id { get; private set; }
    public string Group { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    public string Desc { get; private set; }
}
public class SetupAttributeCollection : List<SetupAttribute>
{
    public SetupAttributeCollection() : base()
    {

    }
    public SetupAttributeCollection(IEnumerable<SetupAttribute> attributes)
    {
        this.AddRange(attributes);
    }
}
