namespace QuickShared.Infrastructure.Services.Storage;

internal sealed record StorageTelegramOptions
{
    public long ChatId { get; init; }
    public required string Token { get; init; }
    public required string UrlBaseToGetFilePath { get; init; }
    public required string UrlBaseToFile { get; init; }
}
