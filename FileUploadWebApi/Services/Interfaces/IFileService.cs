using FileUploadWebApi.Entities;
using FileUploadWebApi.Enums;

namespace FileUploadWebApi.Services.Interfaces
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);
        public Task PostMultiFileAsync(List<FileUploadModel> fileData);
        public Task DownloadFileById(int id);
    }
}