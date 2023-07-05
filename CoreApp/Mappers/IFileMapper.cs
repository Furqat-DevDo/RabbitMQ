using CoreApp.Entities;
using CoreApp.Models;

namespace CoreApp.Mappers;

public interface IFileMapper
{
    public FileModel ToModel(FileInformation file );
}