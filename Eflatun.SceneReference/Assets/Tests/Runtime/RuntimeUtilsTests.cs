using System;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using Eflatun.SceneReference.Utility;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class RuntimeUtilsTests
    {
        [Flags]
        private enum FooFlag
        {
            Foo = 1 << 0,
            Bar = 1 << 1,
            Baz = 1 << 2,
        }

        [Test]
        public void WithoutExtension_Works()
        {
            Assert.AreEqual("foo/bar/baz", "foo/bar/baz.quax".WithoutExtension());
            Assert.AreEqual("foo", "foo.bar".WithoutExtension());
            Assert.AreEqual("foo", "foo".WithoutExtension());
            Assert.AreEqual("", "".WithoutExtension());
            Assert.AreEqual("", ".foo".WithoutExtension());
            Assert.AreEqual(".foo", ".foo.bar".WithoutExtension());
            Assert.AreEqual("a/b/.foo", "a/b/.foo.bar".WithoutExtension());
            Assert.AreEqual("foo.bar", "foo.bar.baz".WithoutExtension());
            Assert.AreEqual("foo/bar/baz.a", "foo/bar/baz.a.b".WithoutExtension());
        }

        [Test]
        public void IncludesFlag_Works()
        {
            const FooFlag all = FooFlag.Foo | FooFlag.Bar | FooFlag.Baz;
            const FooFlag fooBar = FooFlag.Foo | FooFlag.Bar;
            const FooFlag none = 0;

            Assert.IsTrue(all.IncludesFlag(none));
            Assert.IsTrue(all.IncludesFlag(FooFlag.Foo));
            Assert.IsTrue(all.IncludesFlag(FooFlag.Foo | FooFlag.Bar));
            Assert.IsTrue(all.IncludesFlag(all));

            Assert.IsTrue(fooBar.IncludesFlag(none));
            Assert.IsTrue(fooBar.IncludesFlag(FooFlag.Foo));
            Assert.IsTrue(fooBar.IncludesFlag(FooFlag.Foo | FooFlag.Bar));
            Assert.IsFalse(fooBar.IncludesFlag(FooFlag.Baz));
            Assert.IsFalse(fooBar.IncludesFlag(FooFlag.Foo | FooFlag.Baz));

            Assert.IsTrue(none.IncludesFlag(none));
            Assert.IsFalse(none.IncludesFlag(FooFlag.Foo));
            Assert.IsFalse(none.IncludesFlag(FooFlag.Foo | FooFlag.Bar));
        }

        [Test]
        public void BeforeLast_Works()
        {
            Assert.AreEqual("foo", "foo-bar".BeforeLast('-'));
            Assert.AreEqual("foo", "foo-".BeforeLast('-'));
            Assert.AreEqual("foo", "foo".BeforeLast('-'));

            Assert.AreEqual("foo-bar", "foo-bar-baz".BeforeLast('-'));
            Assert.AreEqual("foo-bar", "foo-bar-".BeforeLast('-'));

            Assert.AreEqual("", "-".BeforeLast('-'));
            Assert.AreEqual("", "-foo".BeforeLast('-'));

            Assert.AreEqual("-foo", "-foo-bar".BeforeLast('-'));
        }

        [Test]
        public void IsValidGuid_Works()
        {
            Assert.IsTrue("eb2424ee6dbe9094e9637f087446b90f".IsValidGuid());
            Assert.IsTrue("EB2424EE6DBE9094E9637F087446B90F".IsValidGuid());
            Assert.IsTrue("00000000000000000000000000000000".IsValidGuid());

            Assert.IsFalse("".IsValidGuid());

            // too short
            Assert.IsFalse("eb2424ee6d".IsValidGuid());

            // too long
            Assert.IsFalse("eb2424ee6dbe9094e9637f087446b90fabcde1234".IsValidGuid());

            // non-hex char at the beginning
            Assert.IsFalse("xb2424ee6dbe9094e9637f087446b90f".IsValidGuid());
        }

        [Test]
        public void GuardGuidAgainstNullOrWhitespace_Works()
        {
            Assert.AreEqual(TestUtils.AllZeroGuid, ((string)null).GuardGuidAgainstNullOrWhitespace());
            Assert.AreEqual(TestUtils.AllZeroGuid, "".GuardGuidAgainstNullOrWhitespace());
            Assert.AreEqual(TestUtils.AllZeroGuid, "    ".GuardGuidAgainstNullOrWhitespace());

            const string validGuid = "eb2424ee6dbe9094e9637f087446b90f";
            Assert.AreEqual(validGuid, validGuid.GuardGuidAgainstNullOrWhitespace());

            const string invalidGuid = "abcdefx9";
            Assert.AreEqual(invalidGuid, invalidGuid.GuardGuidAgainstNullOrWhitespace());
        }

        [Test]
        public void IsAddressablesPackagePresent_Works()
        {
            Assert.AreEqual(TestUtils.IsAddressablesPackagePresent, Eflatun.SceneReference.Utility.Utils.IsAddressablesPackagePresent);
        }
    }
}
