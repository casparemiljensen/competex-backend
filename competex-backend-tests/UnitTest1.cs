namespace competex_backend_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicTest()
        {
            var expected = 2;
            var actual = 1 + 1;

            Assert.IsTrue(expected == actual);
            //hello
        }
    }
}