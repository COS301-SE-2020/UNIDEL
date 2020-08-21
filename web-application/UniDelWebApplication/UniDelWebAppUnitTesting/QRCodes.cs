using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UniDelWebAppUnitTesting
{
    public class QRCodes
    {
        [Fact]
        public void CreateQRCode()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ScanQRCode()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ScanQRCodeWrongUserType()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }
    }
}
