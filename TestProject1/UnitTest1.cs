using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private sealed class Field : IReadOnlyList<Field>
        {
            Field IReadOnlyList<Field>.this[int index]
            {
                get
                {
                    if (index != 0)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    return this;
                }
            }

            int IReadOnlyCollection<Field>.Count => 1;

            IEnumerator<Field> IEnumerable<Field>.GetEnumerator()
            {
                yield return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                yield return this;
            }
        }

        [Fact]
        public void Test1()
        {
            Field x = new Field();

            // This works as expected:
            Assert.Same(x, x);

            // This does not work: The active test run was aborted. Reason: Test host process crashed : Stack overflow.
            Assert.Equal(x, x);
        }
    }
}
