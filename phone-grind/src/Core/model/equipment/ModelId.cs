using System.Collections.Generic;

namespace  Game.src.Core.model;

public record ModelId
{
    public string Category{get;}
    public string Entry{get;}
    public ModelId(string category,string entry)
    {
        Category = category;
        Entry = entry;
    }
}