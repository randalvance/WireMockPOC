using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace WireMockPOC.Tests
{
    public class FunctionalTests
    {
        string funcationalTestPaths = @"C:\Development\WireMockPOC\functional-tests\";

        public FunctionalTests()
        {

        }

        [Theory]
        [ClassData(typeof(TestDirectoryClassData))]
        public void When_Comparing_Outputs_Expect_ResutlsToMatch(string testFolder)
        {
            using (var server = CreateServer(testFolder))
            {
                Debug.WriteLine(testFolder);
                var port = server.Ports.FirstOrDefault();
                Debug.WriteLine(server.Urls.First());
                Environment.SetEnvironmentVariable("App__ApiEndpoint", $"http://localhost:{port}");
                var exePath = Path.Combine(@"C:\Development\WireMockPOC\src\WireMockPOCClient\publish", "WireMockPOCClient.exe");
                var process = new Process();
                process.StartInfo.FileName = exePath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    Debug.WriteLine(line);
                }
                process.WaitForExit();
            }
        }

        private void PrintData(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static FluentMockServer CreateServer(string testFolder)
        {
            var server = FluentMockServer.Start(5101);

            server.Given(
                Request.Create().WithPath("/test").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(testFolder.EndsWith("1") ? "{ \"message\": \"Hello World\" }" : "{ \"message\": \"Goodbye World!\" }"));

            return server;
        }
    }
}
