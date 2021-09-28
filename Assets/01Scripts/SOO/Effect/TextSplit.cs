using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections.Generic;

public class TextSplit : MonoBehaviour
{
    public TextSplit(string str)
    {
        textEffect = new Dictionary<string, string>();
        DivideWithTags(str);
    }

    public Dictionary<string, string> textEffect { get; private set; }

    private string DivideWithTags(string str)
    {
        Regex regex = new Regex(@"<(\w+)>([\s\S]+?)<\/\1>");
        Regex regex1 = new Regex(@"<(\w+)>[\s\S]+?<\/\1>");
        string[] strTags = regex1.Split(str);
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
            return "";
        }
        else
        {
            List<string> newArray = new List<string>();
            foreach (string value in strs)
            {
                if (value == strTags[1])
                    continue;
                newArray.Add(value);
            }
            textEffect.Add(strTags[1], strs[index]);

            return SOO.Util.StringBuilder(newArray.ToArray());
        }
    }
}
