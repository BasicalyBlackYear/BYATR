#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemEditor_EnumBuilder : BASICSingleton<ItemEditor_EnumBuilder>
{
    private string enumPath = "Assets/_BASIC/BASICTools/BASICItemEditor/ItemEnum.cs";

    private Dictionary<string, int> itemData = new Dictionary<string, int>();

    private string newEnumString; 

    public void beginBuild(ItemProfile2 profile)
    {
        itemData.Clear();
        foreach (ItemInfo item in profile.items)
        {
            itemData.Add(item.Name, item.Value);
        }

        //build enum
        newEnumString = string.Empty;
        newEnumString = "public enum BASICItem\n{\n";

        foreach (ItemInfo item in profile.items)
        {
            if (item.Value == 0)
                continue; 

            newEnumString += $"    {item.Name} = {item.Value},\n"; 
        }

        newEnumString += "}";

        applyNewEnum(); 
    }

    private void applyNewEnum()
    {
        File.WriteAllText(enumPath, newEnumString);
        AssetDatabase.Refresh();

        Destroy(this.gameObject);
    }
}
#endif