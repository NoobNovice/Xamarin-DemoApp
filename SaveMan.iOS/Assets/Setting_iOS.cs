using System;
using System.IO;
using SaveMan.iOS.Assets;
using SaveMan.Setting;

[assembly: Xamarin.Forms.Dependency(typeof(Setting_iOS))]
namespace SaveMan.iOS.Assets
{
    public class Setting_iOS : ISetting
    {
        public Setting_iOS()
        {

        }

        public FileStream ReadFileStream()
        {
            var fileName = "setting.bin";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            return new FileStream(path, FileMode.Open);
        }

        public FileStream WriteFileStream()
        {
            var fileName = "setting.bin";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            return new FileStream(path, FileMode.Create);
        }
    }
}
