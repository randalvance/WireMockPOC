using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WireMockPOC.Tests
{
    public class TestDirectoryClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string functionalTestsPath = @"C:\Development\WireMockPOC\functional-tests\";

            DirectoryInfo functionTestsDirectory = new DirectoryInfo(functionalTestsPath);

            foreach(var path in functionTestsDirectory.GetDirectories())
            {
                yield return new object[] { path.Name };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
