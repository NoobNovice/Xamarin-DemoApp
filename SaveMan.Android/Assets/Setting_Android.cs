using System;
using System.IO;
using SaveMan.Droid.Assets;
using SaveMan.Setting;

[assembly: Xamarin.Forms.Dependency(typeof(Setting_Android))]
namespace SaveMan.Droid.Assets
{
    public class Setting_Android : ISetting
    {
        public Setting_Android()
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
