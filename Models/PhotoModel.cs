using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SortImagesIntoFolders.Models
{
	public class PhotoModel
	{
        private readonly Uri _source;

        public PhotoModel(string path)
        {
            Source = path;
            _source = new Uri(path);
            Image = BitmapFrame.Create(_source);
            Metadata = new ExifMetadata(_source);
        }

        public string Source { get; }
        public BitmapFrame Image { get; set; }
        public ExifMetadata Metadata { get; }

        public override string ToString() => _source.ToString();
    }
}
