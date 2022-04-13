using System;
using System.Collections.Generic;
using System.IO;
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
            FileName = Path.GetFileName(Source);
            Extension = Path.GetExtension(Source);
            Width = Image.PixelWidth;
            Height = Image.PixelHeight;

            // I couldn't find file size in BitmapImage
            FileInfo fi = new FileInfo(Source);
            Size = fi.Length;
        }

        /// <summary>
        /// A description for the image.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The image file name such as image.png
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file name extension: bmp, gif, jpg, png, tiff, etc...
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The image height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The image width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The file size of the image.
        /// </summary>
        public long Size { get; set; }

        public string Source { get; }
        public BitmapFrame Image { get; set; }
        public ExifMetadata Metadata { get; }

        public override string ToString() => _source.ToString();
    }
}
