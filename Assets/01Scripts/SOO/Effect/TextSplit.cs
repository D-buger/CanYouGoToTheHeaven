using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections.Generic;

public class TextSplit : MonoBehaviour
{
    public TextSplit(string str)
    {
        TextEffect = new Dictionary<string, SplittedText>();
        Tags = new List<string>();
        DivideWithTags(str);
        TextWithoutTags = removeTags.Replace(str, "");
    }

    public Dictionary<string, SplittedText> TextEffect { get; private set; }
    public string TextWithoutTags { get; private set; }
    public List<string> Tags { get; private set; }

    Regex regex = new Regex(@"<(\w+)>([\s\S]+?)<\/\1>");
    Regex tag = new Regex(@"<(\w+)>[\s\S]+?<\/\1>");
    Regex removeTags = new Regex(@"<[^>]*>");
    private string DivideWithTags(string str)
    {
        string[] strTags = tag.Split(str);
        string[] strs = regex.Split(str);

        if (strs.Length < 2)
            return str;

        if (regex.IsMatch(strs[2]))
        {
           string untaggedText = DivideWithTags(strs[2]);
            Tags.Add(strTags[1]);
           TextEffect.Add(strTags[1], new SplittedText(TextWithoutTags, 
               strs[0].Length + strs[1].Length - 1,
               removeTags.Replace(untaggedText, "").Length));

            return RemoveElementInText(strs, strTags[1]);
        }
        else
        {
            Tags.Add(strTags[1]);
            TextEffect.Add(strTags[1], new SplittedText(TextWithoutTags,
               strs[0].Length + strs[1].Length - 1,
               removeTags.Replace(strs[2], "").Length));

            return strs[2];
        }
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

public struct SplittedText
{
    public SplittedText(string _untagOri, int _startIndex, int _count)
    {
        untaggedOriginalText = _untagOri;
        startIndex = _startIndex;
        count = _count;
    }

    public string untaggedOriginalText;
    public int startIndex;
    public int count;
}