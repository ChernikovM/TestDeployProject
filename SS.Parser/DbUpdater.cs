using Microsoft.Extensions.DependencyInjection;
using SS.DataAccess.Entities;
using SS.DataAccess.Repositories.EF.Interfaces;
using SS.Parser.Base;

namespace SS.Parser;

public class DbUpdater : IDbUpdater
{
    private readonly IStickerPackRepository _stickerPackRepository;
    private readonly ILabelRepository _labelRepository;
    private readonly IServiceProvider _serviceProvider;

    private int _chunkSize = 50; // TODO: create ParserSettings

    public DbUpdater(
        IStickerPackRepository stickerRepository, ILabelRepository labelRepository, IServiceProvider serviceProvider)
    {
        this._stickerPackRepository = stickerRepository;
        this._labelRepository = labelRepository;
        this._serviceProvider = serviceProvider;
    }

    public async Task<bool> UpdateAsync()
    {
        List<IParserBase> parsers = this.GetParsers();

        if (parsers?.Any() is true)
        {
            foreach (IParserBase parser in parsers)
            {
                List<Models.StickerPack> stickerPacks;

                try
                {
                    stickerPacks = await parser.ParseAsync();
                }
                catch (Exception e)
                {
                    // TODO: add logger

                    throw;
                }

                if (stickerPacks?.Any() is true)
                {
                    List<StickerPack> chunk =
                        new List<StickerPack>(this._chunkSize);

                    foreach (Models.StickerPack pack in stickerPacks)
                    {
                        if (!await this._stickerPackRepository
                                .ExistAsync(stickerPack => stickerPack.Name == pack.Name))
                        {
                            chunk.Add(await this.MapStickerPack(pack));

                            if (chunk.Count == this._chunkSize)
                            {
                                await this._stickerPackRepository
                                    .AddRangeAsync(chunk);

                                await this._stickerPackRepository.SaveChanges();

                                chunk.RemoveAll(_ => true);
                            }
                        }
                    }

                    if (chunk.Any())
                    {
                        await this._stickerPackRepository
                            .AddRangeAsync(chunk);

                        await this._stickerPackRepository.SaveChanges();

                        chunk.RemoveAll(_ => true);
                    }
                }
            }
        }
        else
        {
            throw new ApplicationException("No available registered parsers.");
        }

        return true;
    }

    private List<IParserBase> GetParsers()
    {
        return this._serviceProvider
            .GetServices<IParserBase>()
            .ToList();
    }

    private async Task<StickerPack> MapStickerPack(Models.StickerPack entryEntity)
    {
        StickerPack result = new StickerPack
        {
            Name = entryEntity.Name,
            Count = entryEntity.Count,
            SourceLink = entryEntity.SourceLink,
            TelegramLink = entryEntity.TelegramLink
        };

        if (entryEntity.Labels?.Any() is true)
        {
            List<Label> labels = new List<Label>();

            foreach (string labelString in entryEntity.Labels)
            {
                Label label = (await this._labelRepository
                                  .FindAsync(entity => entity.Name == labelString))
                              ?.FirstOrDefault() ??
                              (await this._labelRepository
                                  .AddAsync(new Label
                                  {
                                      Name = labelString
                                  })).Entity;

                await this._labelRepository.SaveChanges();

                labels.Add(label);
            }

            result.Labels = labels;
        }

        return result;
    }
}