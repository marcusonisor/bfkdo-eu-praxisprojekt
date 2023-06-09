using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class BenutzerTests
    {
        private string _url;

        [SetUp]
        public void Setup()
        {
            _url = "http://localhost:5194";
        }


    }
}
