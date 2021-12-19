using RageIO;
using System.Collections.Generic;
using System.IO;

namespace RagePackage.Commands
{
    public class FileMoveCommand : Command
    {
        public string From { get; set; }
        public string To { get; set; }

        public override void Execute()
        {
            using(FileStream fromStream = File.OpenRead(From))
            {
                FileIO file = new FileIO(To);
                using(Stream toStream = file.Open(true))
                {
                    fromStream.CopyTo(toStream);
                    toStream.Flush();
                }
            }
        }
    }

    public class FilesMoveCommand : Command
    {
        public string From { get; set; }
        public string To { get; set; }

        public List<string> Files { get; set; }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

    public class FileRenameCommand : Command
    {
        public string Path { get; set; }
        public string NewName { get; set; }

        public override void Execute()
        {
            //FileIO file = new FileIO(Path);
            throw new System.NotImplementedException();
        }
    }

    public class FileDeleteCommand : Command
    {
        public string Path { get; set; }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
