﻿using FileUploadWebApi.Data;
using FileUploadWebApi.Entities;
using FileUploadWebApi.Enums;
using FileUploadWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileUploadWebApi.Services
{
    public class FileService : IFileService
    {
        private readonly FileUploadDbContext _context;
        public FileService(FileUploadDbContext context)
        {
            _context = context;
        }

        public async Task DownloadFileById(int id)
        {
            try
            {
                var file = _context.FileDetails.Where(t => t.ID == id).FirstOrDefaultAsync();
                var content = new System.IO.MemoryStream(file.Result.FileData);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "FileDownloaded", file.Result.FileName);
                await CopyStream(content, path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
        {
            try
            {
                var fileDetails = new FileDetails
                {
                    ID = 0,
                    FileName = fileData.FileName,
                    FileType = fileType
                };
                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }
                var result = _context.FileDetails.Add(fileDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task PostMultiFileAsync(List<FileUploadModel> fileData)
        {
            try
            {
                foreach (FileUploadModel file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        ID = 0,
                        FileName = file.FileDetails.FileName,
                        FileType = file.FileType
                    };
                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }
                    var result = _context.FileDetails.Add(fileDetails);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
