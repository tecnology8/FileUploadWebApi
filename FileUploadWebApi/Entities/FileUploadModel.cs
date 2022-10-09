﻿using FileUploadWebApi.Enums;

namespace FileUploadWebApi.Entities
{
    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}