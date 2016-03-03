// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Text;
    using Log;
    using Xunit;

    [Collection("Network"), Trait("Category", "Network")]
    public class DesktopNetworkManagerTest
    {
        #region Unit tests
        [Fact]
        public void CreateDesktopNetworkListenerWithInvalidAddressString_ThrowsFormatException()
        {
            NetworkManager.SetImplementation(new DesktopNetworkManager());
            var port = Ports.GetNext();
            Assert.Throws<FormatException>(() => NetworkManager.CreateNetworkListener(port, "not an ip address"));
        }

        [Fact]
        public void CreateServerBoundToIP_Succeeds()
        {
            NetworkManager.SetImplementation(new DesktopNetworkManager());
            var port = Ports.GetNext();
            using (var server = new DicomServer<DummyService>(port, null, null, null, null, "0.0.0.0"))
            {
                server.Stop();
            }
        }

        [Fact]
        public void CreateServerBoundToIP_ThrowsFormatException()
        {
            NetworkManager.SetImplementation(new DesktopNetworkManager());
            var port = Ports.GetNext();
            Assert.Throws<FormatException>(() => new DicomServer<DummyService>(port, null, null, null, null, "to be or not to be"));
        }

        #endregion

        #region Support classes
        private class DummyService : DicomService, IDicomServiceProvider
        {
            protected DummyService(INetworkStream stream, Encoding fallbackEncoding, Logger log) : base(stream, fallbackEncoding, log)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
                throw new NotImplementedException();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
                throw new NotImplementedException();
            }

            public void OnReceiveAssociationReleaseRequest()
            {
                throw new NotImplementedException();
            }

            public void OnReceiveAssociationRequest(DicomAssociation association)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}