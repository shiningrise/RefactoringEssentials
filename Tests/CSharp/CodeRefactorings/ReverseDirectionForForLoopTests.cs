using NUnit.Framework;
using RefactoringEssentials.CSharp.CodeRefactorings;

namespace RefactoringEssentials.Tests.CSharp.CodeRefactorings
{
    [TestFixture]
    public class ReverseDirectionForForLoopTests : CSharpCodeRefactoringTestBase
    {
        [Test]
        public void TestPrimitveExpression()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; i < 10; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 9; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestPrimitveExpressionCase2()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; 10 > i; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 9; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestPrimitveExpressionReverse()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 9; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 0; i < 10; i++)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestLowerEqualPrimitveExpression()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; i <= 10; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 10; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestLowerEqualPrimitveExpressionCase2()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; 10 >= i; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 10; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestArbitraryBounds()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar (int from, int to)
    {
        $for (int i = from; i < to; i += 1)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar (int from, int to)
    {
        for (int i = to - 1; i >= from; i--)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestArbitraryBoundsCase2()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; i < a.T; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = a.T - 1; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }


        [Test]
        public void TestComplex()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = Foo().Bar + Test; i < (to - from).Length; i += a + b)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = (to - from).Length - (a + b); i >= Foo().Bar + Test; i -= a + b)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestComplexReverse()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = (to - from).Length - (a + b); i >= Foo().Bar + Test; i -= a + b)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = Foo().Bar + Test; i < (to - from).Length; i += a + b)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestArbitraryBoundsReverse()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar (int from, int to)
    {
        $for (int i = to - 1; i >= from; --i)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar (int from, int to)
    {
        for (int i = from; i < to; i++)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestPrimitiveExpressionArbitrarySteps()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; i < 100; i += 5)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 95; i >= 0; i -= 5)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestPrimitiveExpressionArbitraryStepsCase2()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0; i <= 100; i += 5)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 100; i >= 0; i -= 5)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestPrimitiveExpressionArbitraryStepsReverse()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 95; i >= 0; i -= 5)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 0; i < 100; i += 5)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestArbitrarySteps()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar (int from, int to, int step)
    {
        $for (int i = from; i < to; i += step)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar (int from, int to, int step)
    {
        for (int i = to - step; i >= from; i -= step)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Test]
        public void TestArbitraryStepsReverse()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar (int from, int to, int step)
    {
        $for (int i = to - step; i >= from; i -= step)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar (int from, int to, int step)
    {
        for (int i = from; i < to; i += step)
            System.Console.WriteLine(i);
    }
}
");
        }

        [Ignore("Implement me")]
        [Test]
        public void TestOptimizedFor()
        {
            Test<ReverseDirectionForForLoopCodeRefactoringProvider>(@"
class Foo
{
    public void Bar ()
    {
        $for (int i = 0, upper = 10; i < upper; i++)
            System.Console.WriteLine(i);
    }
}
", @"
class Foo
{
    public void Bar ()
    {
        for (int i = 9; i >= 0; i--)
            System.Console.WriteLine(i);
    }
}
");
        }


    }
}

