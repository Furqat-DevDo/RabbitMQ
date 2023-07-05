using CoreApp.Entities;
using CoreApp.Models;

namespace CoreApp.Mappers;

public class FileMapper : IFileMapper
{
    public FileModel ToModel(FileInformation file)
    {
        var model = new FileModel
        {
            Id = file.Id,
            Extension = file.Extension,
            SavedDate = file.SavedDate
        };
        
        return model;
    }
}