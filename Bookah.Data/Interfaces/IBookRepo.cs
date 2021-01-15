using Bookah.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bookah.Data.Interfaces
{
    public interface IBookRepo
    {
        Task Create(Book book);
        void Upload(FileStream stream, string file);
    }
}
