using HtmlAgilityPack;

namespace SS.Parser.Chpic;

internal static class Extensions
{
    public static HtmlNode GetChildByClassNameOrDefault(this HtmlNode parentNode, string className)
    {
        HtmlNode child = null;

        if (parentNode is not null && parentNode.HasChildNodes)
        {
            child = parentNode
                .ChildNodes
                ?.FirstOrDefault(
                    ch => ch.HasAttributes &&
                          ch.Attributes
                              .ToList()
                              .Exists(
                                  attr => attr.Name == "class" &&
                                          attr.Value == className));
        }

        return child;
    }

    public static string GetAttributeByNameOrDefault(this HtmlNode node, string attributeName)
    {
        string value = null;

        if (node is not null && node.HasAttributes)
        {
            value = node
                .Attributes
                .ToList()
                .FirstOrDefault(
                    attr => attr.Name == attributeName)
                ?.Value;
        }

        return value;
    }
}