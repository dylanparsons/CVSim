using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Program.Unit.Test
{
    public class FakeFileStream : Stream
    {
        private List<string> _simulatedFile;

        public FakeFileStream()
        {
            _simulatedFile = new List<string>();
        }

        public bool WasWrittenTo { get; private set; }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var itemString = Encoding.Default.GetString(buffer);

            _simulatedFile.Add(itemString);

            WasWrittenTo = true;
        }

        public bool AreAllParametersCorrect
        {
            get
            {
                var result = (_simulatedFile.Count == 6);
                result = result && (_simulatedFile[0].StartsWith("# CV_EC: nt/p-unit, K ="));
                result = result && (_simulatedFile[1].StartsWith("# pstart, prev ="));
                result = result && (_simulatedFile[2].StartsWith("# No. of conc. nodes between boundaries:"));
                result = result && (_simulatedFile[3].StartsWith("# X(1), Xlim ="));
                result = result && (_simulatedFile[4].StartsWith("# gamma ="));
                result = result && (_simulatedFile[5].StartsWith("#   p       G(dir),    G(trans)"));

                return result;
            }
        }

        public string this[ int index ] => _simulatedFile[index];

        public int Count => _simulatedFile.Count;

        public override void Flush()
        {
            throw new System.NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }
    }
}