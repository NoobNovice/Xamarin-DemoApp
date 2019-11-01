using System;
using System.IO;

namespace SaveMan.Setting
{
    public interface ISetting
    {
        FileStream WriteFileStream();

        FileStream ReadFileStream();
    }
}
