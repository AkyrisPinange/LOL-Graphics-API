using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lolgraphics.Core;
using Lolgraphics.Core.Damain;


namespace Lolgraphics.Tests.Domain
{
    public class MyDomainTest
    {
        private readonly MyDomain _domainService;

        public MyDomainTest()
        {
            _domainService = new MyDomain();
        }

        [Fact]
        public void Multiply_ShouldReturnProduct_WhenGivenTwoNumbers()
        {
            // Arrange
            int a = 3;
            int b = 4;

            // Act
            int result = _domainService.Multiply(a, b);

            // Assert
            Assert.Equal(12, result);
        }
    }
}

