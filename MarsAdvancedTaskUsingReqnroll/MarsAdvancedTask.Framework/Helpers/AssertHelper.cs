using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MarsAdvancedTask.Framework.Helpers
{
    public class AssertHelper(TestState state)  //Primary constructor
    {
        private readonly TestState _state = state;

        public void IsEqualTo(string expected, string actual, string message)
        {
            Assert.That(actual, Is.EqualTo(expected));
            _state.Test.Log(Status.Pass, $"Assertion Passed:  Expected: '{expected}', Actual:'{actual}'");
        }

        public void ListsMatch(List<string> actualList, List<string?> expectedList)
        {
            CollectionAssert.AreEqual(expectedList, actualList, $"Mismatch!\nExpected: {string.Join(", ", expectedList)}\nActual: {string.Join(", ", actualList)}");
            _state.Test.Log(Status.Pass, $"Assertion Passed: Actual list matches expected list.");
        }

        public void AssertToastMessageForInvalid(List<(string messageText, string messageType)> actualMessages, string? expected)
        {
            Assert.Multiple(() =>
            {
                foreach (var (type, text) in actualMessages)
                {
                    Assert.That(type, Is.EqualTo("error"), "Error message should have shown");
                    Assert.That(text, Does.Contain(expected), $"Message contains {expected}, but not found");
                }
            });
        }

        public void AssertMultipleContain(List<string> actualMessages, string expected)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualMessages, Is.Not.Null.And.Not.Empty, "Actual messages list is null or empty.");
                Assert.That(actualMessages.All(m => m.Contains(expected)), Is.True, $"Expected all messages to contain '{expected}', but got:\n - {string.Join("\n - ", actualMessages)}");
            });
            _state.Test.Log(Status.Pass, $"Assertion Passed: '{actualMessages}' contains '{expected}'");
        }

        public void IsNotEqualTo(List<string> actualMessages, string expected)
        {
            foreach (var actual in actualMessages)
            {
                Assert.That(actual, Is.Not.EqualTo(expected));
                _state.Test.Log(Status.Pass, $"Assertion Passed:  Expected: '{expected}', Actual:'{actual}'");
            }
        }

        public void Contains(string actual, string expectedSubstring, string message)
        {
            Assert.That(actual.Contains(expectedSubstring));
            _state.Test.Log(Status.Pass, $"Assertion Passed:{message} - '{actual}' contains '{expectedSubstring}");
        }

        public void IsNotNullOrEmpty(string actual, string message)
        {
            Assert.That(actual, Is.Not.Null.And.Not.Empty, "Actual value is null or empty");
            _state.Test.Log(Status.Info, $"Actual Message:{actual}");
        }

        public void IsNullOrEmpty(string actual, string expected)
        {
            Assert.That(actual, Is.Null.And.Empty, "Actual value is null or empty");
            _state.Test.Log(Status.Info, $"Expected Message:{expected}");
        }

        public void AssertAnyMessageContains(List<string> actualMessages, string expected)
        {
            Assert.That(actualMessages, Is.Not.Null.And.Not.Empty, "Actual messages list is null or empty.");
            Assert.That(expected, Is.Not.Null.And.Not.Empty, "Expected message is null or empty.");
            bool actual = actualMessages.Any(m => m != null && m.Contains(expected));
            Assert.That(actual, Is.True, $"Expected message '{expected}' was not found in actual messages: [{string.Join(", ", actualMessages)}]");
            _state.Test.Log(Status.Pass, $"Assertion Passed: One of the actual messages contains '{expected}'");
        }

        public void ListContainsString(List<string> actualMessages, string? expected)
        {
            foreach (var actual in actualMessages)
            {
                Assert.That(actual, Does.Contain(expected), $"Message:{actualMessages} doesn't contain {expected}");
            }
        }

        public void IsTrue(List<bool> actual)
        {
            Assert.That(actual, Is.Not.Null.And.Not.Empty, "The list is null or empty.");

            // Ensure all values are true
            Assert.That(actual.All(x => x), Is.True, "Message: One or more items in the list are false (cancel failed)");
        }

        public void IsFalse(List<bool> actual)
        {
            Assert.That(actual, Is.Null.And.Empty, "The list is null or empty.");

            // Ensure all values are true
            Assert.That(actual.All(x => x), Is.True, "Message: One or more items in the list are false (cancel failed)");
        }

        public void AssertListContainsAll(List<string> actualMessages, List<string?> expectedMessages)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualMessages, Is.Not.Null.And.Not.Empty, "Actual messages list is null or empty.");

                foreach (var expected in expectedMessages)
                {
                    Assert.That(actualMessages.Any(m => m.Contains(expected)), Is.True, $"Expected message containing '{expected}' was not found. " +
                        $"Actual messages: {string.Join(", ", actualMessages)}"
                    );
                }
            });

            state.Test.Log(Status.Pass, $"Assertion Passed: All expected messages found in actual messages.");
        }

        public void AssertToastMessage(List<(string messageText, string messageType)> actualMessages, string? expected)
        {
            Assert.Multiple(() =>
            {
                foreach (var (text, type) in actualMessages)
                {
                    if (string.Equals(type, "Success", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.That(type, Is.EqualTo("success"), "Success message should have shown");
                        Assert.That(text, Does.Contain(expected), $"Message contains {expected}, but not found");
                    }
                    else if (string.Equals(type, "Error", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.That(type, Is.EqualTo("error"), "Error message should have shown");
                        Assert.That(text, Does.Contain(expected), $"Message contains {expected}, but not found");
                    }
                }
            });
        }

        public void AssertListHasUrlWith(List<string> urls, string part)
        {
            Assert.That(urls.Any(u => u.Replace("&", "").Contains(part)), Is.True);
        }
    }
}
