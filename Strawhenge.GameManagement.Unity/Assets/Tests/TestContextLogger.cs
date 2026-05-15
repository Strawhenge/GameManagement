using NUnit.Framework;
using Strawhenge.Common.Logging;
using System;

namespace Strawhenge.GameManagement.Unity.Tests
{
    class TestContextLogger : ILogger
    {
        public void LogInformation(string message) => TestContext.Progress.WriteLine($"[Info] {message}");

        public void LogWarning(string message) => TestContext.Progress.WriteLine($"[Warning] {message}");

        public void LogError(string message) => TestContext.Progress.WriteLine($"[Error] {message}");

        public void LogException(Exception exception) => TestContext.Progress.WriteLine($"[Exception] {exception}");
    }
}