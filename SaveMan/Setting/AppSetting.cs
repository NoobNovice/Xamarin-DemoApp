using System;
using System.IO;
using Xamarin.Forms;

namespace SaveMan.Setting
{
    public class AppSetting
    {
        private string _appVersion = "1.0";
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
        }

        private string _currentMount = DateTime.Now.ToString("MMyyyy");
        public string CurrentMount
        {
            get
            {
                return _currentMount;
            }
            set
            {
                _currentMount = value;
                WriteSetting();
            }
        }

        private bool _firstUse = true;
        public bool FirstUse
        {
            get
            {
                return _firstUse;
            }
            set
            {
                _firstUse = value;

                WriteSetting();
            }
        }

        public AppSetting()
        {
            try
            {
                ReadSetting();
            }
            catch (IOException)
            {
                System.Diagnostics.Debug.WriteLine("AppSetting: Create new file setting");

                WriteSetting();

                System.Diagnostics.Debug.WriteLine("AppSetting: Create new file setting success");
            }
        }

        private void WriteSetting()
        {
            try
            {
                var writeStream = DependencyService.Get<ISetting>().WriteFileStream();
                using (BinaryWriter bw = new BinaryWriter(writeStream))
                {
                    bw.Write(_appVersion);
                    bw.Write(_currentMount);
                    bw.Write(_firstUse);
                    bw.Close();
                }
            }
            catch(Exception e)
            {
                throw new Exception("AppSetting: WriteSetting at line 70 fail => " + e.Message);
            }
        }

        private void ReadSetting()
        {
            try
            {
                var readStream = DependencyService.Get<ISetting>().ReadFileStream();
                using (BinaryReader br = new BinaryReader(readStream))
                {
                    _appVersion = br.ReadString();
                    _currentMount = br.ReadString();
                    _firstUse = br.ReadBoolean();
                    br.Close();
                }
            }
            catch(Exception e) when (!(e is IOException))
            {
                throw new Exception("AppSetting: ReadSetting at line 97 fail => " + e.Message);
            }
        }
    }
}
