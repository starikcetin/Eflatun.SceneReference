using System.IO;
using Eflatun.SceneReference.Utility;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class ConvertedPathTests
    {
        [Test]
        public void GivenPath_Works()
        {
            Assert.AreEqual(@"foo/bar/baz", new ConvertedPath(@"foo/bar/baz").GivenPath);
            Assert.AreEqual(@"foo\bar\baz", new ConvertedPath(@"foo\bar\baz").GivenPath);
            Assert.AreEqual(@"foo/bar\baz", new ConvertedPath(@"foo/bar\baz").GivenPath);
            Assert.AreEqual(@"foo", new ConvertedPath(@"foo").GivenPath);
            Assert.AreEqual(@"", new ConvertedPath(@"").GivenPath);
        }

        [Test]
        public void WindowsPath_Works()
        {
            Assert.AreEqual(@"foo\bar\baz", new ConvertedPath(@"foo/bar/baz").WindowsPath);
            Assert.AreEqual(@"foo\bar\baz", new ConvertedPath(@"foo\bar\baz").WindowsPath);
            Assert.AreEqual(@"foo\bar\baz", new ConvertedPath(@"foo/bar\baz").WindowsPath);
            Assert.AreEqual(@"foo", new ConvertedPath(@"foo").WindowsPath);
            Assert.AreEqual(@"", new ConvertedPath(@"").WindowsPath);
        }

        [Test]
        public void UnixPath_Works()
        {
            Assert.AreEqual(@"foo/bar/baz", new ConvertedPath(@"foo/bar/baz").UnixPath);
            Assert.AreEqual(@"foo/bar/baz", new ConvertedPath(@"foo\bar\baz").UnixPath);
            Assert.AreEqual(@"foo/bar/baz", new ConvertedPath(@"foo/bar\baz").UnixPath);
            Assert.AreEqual(@"foo", new ConvertedPath(@"foo").UnixPath);
            Assert.AreEqual(@"", new ConvertedPath(@"").UnixPath);
        }

        [Test]
        public void PlatformPath_Works()
        {
            var dsc = Path.DirectorySeparatorChar;
            Assert.AreEqual($@"foo{dsc}bar{dsc}baz", new ConvertedPath(@"foo/bar/baz").PlatformPath);
            Assert.AreEqual($@"foo{dsc}bar{dsc}baz", new ConvertedPath(@"foo\bar\baz").PlatformPath);
            Assert.AreEqual($@"foo{dsc}bar{dsc}baz", new ConvertedPath(@"foo/bar\baz").PlatformPath);
            Assert.AreEqual(@"foo", new ConvertedPath(@"foo").PlatformPath);
            Assert.AreEqual(@"", new ConvertedPath(@"").PlatformPath);
        }
    }
}
