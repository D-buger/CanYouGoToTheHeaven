using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections.Generic;

public class TextSplit : MonoBehaviour
{
    public TextSplit(string str)
    {
        textEffect = new Dictionary<string, string>();
        TextWithoutTags = DivideWithTags(str);
    }

    public Dictionary<string, string> textEffect { get; private set; }
    public string TextWithoutTags { get; private set; }

    public bool StringToCount(string type ,out int startIndex, out int count)
    {
        startIndex = count = -1;
        if (!textEffect.ContainsKey(type))
            return false;

        string[] split = TextWithoutTags.Split(textEffect[type].ToCharArray());

        count = textEffect[type].Length;

        return true;
    }

    private string DivideWithTags(string str)
    {
        Regex regex = new Regex(@"<(\w+)>([\s\S]+?)<\/\1>");
        Regex tag = new Regex(@"<(\w+)>[\s\S]+?<\/\1>");
        string[] strTags = tag.Split(str);
        string[] strs = regex.Split(str);
        int index = 0;
        for (; index < strs.Length; index++)
        {
            if (strs[index] == strTags[1])
            {
                index++;
                break;
            }
        }

        if (regex.IsMatch(strs[2]))
        {
            textEffect.Add(strTags[1], DivideWithTags(strs[2]));
            strs[2] = textEffect[strTags[1]];
        }

        return RemoveElementInText(strs, strTags[1]);
    }

    List<string> newArray = new List<string>();
    private string RemoveElementInText(string[] str, string element)
    {
        newArray.Clear();
        foreach (string value in str)
        {
            if (value == element)
                continue;
            newArray.Add(value);
        }

        return SOO.Util.StringBuilder(newArray.ToArray());
    }
}