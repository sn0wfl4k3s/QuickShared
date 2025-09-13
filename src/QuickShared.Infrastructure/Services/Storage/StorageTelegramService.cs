using Microsoft.Extensions.Options;
using QuickShared.Domain.Services;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuickShared.Infrastructure.Services.Storage;

internal sealed class StorageTelegramService(IOptions<StorageTelegramOptions> options, HttpClient httpClient) : IStorageService
{
    private readonly StorageTelegramOptions _options = options.Value;
    private readonly HttpClient _httpClient = httpClient;

    public async ValueTask<byte[]> GetFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetByteArrayAsync(fileUrl, cancellationToken);
    }

    public async ValueTask<UploadFileResponse> SaveFileAsync(UploadFileCommand request, CancellationToken cancellationToken = default)
    {
        var fileId = Guid.NewGuid();

        var filename = $"{fileId}{Path.GetExtension(request.FileName)}";

        var inputFile = new InputFileStream(request.FileStream, filename);

        var telegramBot = new TelegramBotClient(_options.Token);

        var message = await telegramBot.SendDocument(_options.ChatId, inputFile, cancellationToken: cancellationToken);

        using var httpClient = new HttpClient();

        var urlGetFilePath = string.Format(_options.UrlBaseToGetFilePath, _options.Token, message?.Document?.FileId);

        var json = await httpClient.GetStringAsync(urlGetFilePath, cancellationToken);

        var filePath = JsonDocument.Parse(json).RootElement
            .GetProperty("result")
            .GetProperty("file_path")
            .GetString()
            ?? string.Empty;

        var fileUrl = string.Format(_options.UrlBaseToFile, _options.Token, filePath);

        return new UploadFileResponse(fileId, fileUrl);
    }
}