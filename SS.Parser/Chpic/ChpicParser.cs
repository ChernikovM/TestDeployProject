using HtmlAgilityPack;
using SS.Parser.Base;
using SS.Parser.Models;

namespace SS.Parser.Chpic;

public class ChpicParser : IParserBase
{
    public ChpicParser()
    {
    }

    private const string BaseUrl = "https://chpic.su";
    private const string UrlTemplate = BaseUrl + "/ru/stickers/?sortby=date&page={0}";

    private const string AdblockDivContent = "adblock collection_list_adblock";

    // TODO: add parsing bu chunks
    public async Task<List<StickerPack>> ParseAsync()
    {
        bool needRequest = true;
        int errorCounter = 0;

        List<StickerPack> parseResult = new List<StickerPack>();

        int page = 0;

        do
        {
            List<StickerPack> pageResult = null;

            try
            {
                pageResult = await this.ParsePageAsync(page++);

                if (pageResult?.Any() is true)
                {
                    parseResult.AddRange(pageResult);
                }
            }
            catch (Exception)
            {
                // TODO: add logger
                errorCounter++;

                if (errorCounter == 3)
                {
                    needRequest = false;
                }
            }

            if (needRequest && (pageResult is null || pageResult.Any() is false)) // exc not occured 3 times, but response is empty (last page)
            {
                needRequest = false;
            }
        }
        while (needRequest);

        return parseResult;
    }

    public async Task<List<StickerPack>> ParsePageAsync(int page)
    {
        Console.Write($"[CHPIC] Parsing {page} page...");

        List<StickerPack> result = null;
        List<HtmlNode> stickerNodes = null;

        try
        {
            HtmlDocument document = await new HtmlWeb()
                .LoadFromWebAsync(string.Format(UrlTemplate, page));

            HtmlNode collectionNode = document.DocumentNode
                ?.SelectNodes("//div[@class='collections_list']")
                ?.FirstOrDefault();

            stickerNodes = collectionNode
                ?.ChildNodes
                ?.Where(x => x.Name == "div" && !x.OuterHtml.Contains(AdblockDivContent))
                .ToList();
        }
        catch (Exception exception)
        {
            // TODO: add logger
            Console.WriteLine($" Exception occured: '{exception.Message}' [{exception.StackTrace}].");
        }

        if (stickerNodes?.Any() is true)
        {
            result = stickerNodes
                .Select(this.CreateStickerPack)
                .ToList();
        }

        Console.WriteLine($" {result?.Count ?? 0} stickerPacks parsed.");

        return result;
    }

    private StickerPack CreateStickerPack(HtmlNode stickerNode)
    {
        StickerPack stickerPack = null;

        try
        {
            HtmlNode textNode = stickerNode?.GetChildByClassNameOrDefault("textsblock");

            HtmlNode titleNode = textNode?.GetChildByClassNameOrDefault("title");

            string url = titleNode
                ?.ChildNodes
                ?.FirstOrDefault(node => node.Name == "a")
                ?.GetAttributeByNameOrDefault("href");

            string countString = textNode
                ?.GetChildByClassNameOrDefault("subtitle")
                ?.InnerText
                ?.Split(" ")
                .First();

            stickerPack = new StickerPack
            {
                Name = titleNode?.InnerText,
                SourceLink = BaseUrl + url,
                Count = uint.TryParse(countString, out uint count)
                    ? count
                    : null,
                Labels = this.GetStickerPackStatuses(stickerNode)
            };

            if (!string.IsNullOrWhiteSpace(stickerPack.SourceLink))
            {
                stickerPack.TelegramLink =
                    string.Format(Constants.BaseTelegramUrlTemplate, stickerPack.SourceLink?.Split("/")[^2]);
            }
        }
        catch (Exception)
        {
            // TODO: add logger
        }

        return string.IsNullOrWhiteSpace(stickerPack?.TelegramLink)
            ? null
            : stickerPack;
    }

    private List<string> GetStickerPackStatuses(HtmlNode stickerNode)
    {
        List<string> statuses = null;

        try
        {
            List<HtmlNode> statusNodes = stickerNode
                .GetChildByClassNameOrDefault("imageblock")
                ?.GetChildByClassNameOrDefault("statuses")
                ?.ChildNodes
                ?.ToList();

            if (statusNodes?.Any() is true)
            {
                statuses = new List<string>();

                foreach (HtmlNode statusNode in statusNodes
                             .Where(node => node.Name == "div"))
                {
                    string stat = statusNode.InnerText;

                    if (string.IsNullOrWhiteSpace(stat))
                    {
                        stat = statusNode.GetAttributeByNameOrDefault("title");
                    }

                    if (string.IsNullOrWhiteSpace(stat) is false)
                    {
                        statuses.Add(stat);
                    }
                }
            }
        }
        catch (Exception)
        {
            // TODO: add logger
        }

        return statuses;
    }
}