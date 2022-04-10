using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace SortImagesIntoFolders.Models
{
	public class PhotoCollectionModel : ObservableCollection<PhotoModel>
	{
        private DirectoryInfo _directory;

        public PhotoCollectionModel()
        {
        }

        public PhotoCollectionModel(string path) : this(new DirectoryInfo(path))
        {
        }

        public PhotoCollectionModel(DirectoryInfo directory)
        {
            _directory = directory;
            Update();
        }

        public string Path
        {
            set
            {
                _directory = new DirectoryInfo(value);
                Update();
            }
            get { return _directory.FullName; }
        }

        public DirectoryInfo Directory
        {
            set
            {
                _directory = value;
                Update();
            }
            get { return _directory; }
        }

        private void Update()
        {
            Clear();
            try
            {
                string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
                var files = _directory.GetFiles("*.*").Where(s => supportedExtensions.Contains(s.Extension.ToLower()));
                foreach (var f in files)
                    Add(new PhotoModel(f.FullName));
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("No Such Directory");
            }
        }
    }
}
