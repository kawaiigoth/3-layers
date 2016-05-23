using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _EPAM_UserList.Entites
{
    public class ImageDTO
    {
        public int Id { get; private set; }
        public Guid Name { get; private set; }
        public byte[] DataSmall { get; private set; }
        public byte[] DataBig { get; private set; }
        public string Format { get; private set; }
        public ImageDTO(string format = null, byte[] data_small = null, byte[] data_big = null)
        {
            Name = Guid.NewGuid();
            DataSmall = data_small;
            DataBig = data_big;
            Format = format;
        }
        public ImageDTO(int id, Guid name, string format = null,  byte[] data_small = null, byte[] data_big = null)
        {
            Name = name;
            DataSmall = data_small;
            DataBig = data_big;
            Format = format;
            Id = id;
        }
        
    }
}
