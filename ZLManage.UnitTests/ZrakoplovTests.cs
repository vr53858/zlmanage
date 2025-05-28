using Moq;
using ZLManage.ApplicationServices.Services.Zrakoplov;
using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.UnitTests;

    [TestFixture]
    public class ZrakoplovServiceTests
    {
        private Mock<IZrakoplovRepository> _repoMock = null!;
        private IZrakoplovService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IZrakoplovRepository>();

            var sample = new Zrakoplov {
                Id_zrakoplova = 1,
                Model        = "Airbus A320",
                Registracija = "HR-A320"
            };
            var list = new List<Zrakoplov> { sample };

            _repoMock
                .Setup(r => r.GetZrakoploviAsync())
                .ReturnsAsync(list);

            _repoMock
                .Setup(r => r.GetZrakoplovByIdAsync(1))
                .ReturnsAsync(sample);
            _repoMock
                .Setup(r => r.GetZrakoplovByIdAsync(It.Is<int>(i => i != 1)))
                .ReturnsAsync((Zrakoplov?)null);

            _repoMock
                .Setup(r => r.CreateZrakoplovAsync(It.IsAny<Zrakoplov>()))
                .Callback<Zrakoplov>(e => e.Id_zrakoplova = 99)
                .ReturnsAsync(1);

            _repoMock
                .Setup(r => r.UpdateZrakoplovAsync(It.IsAny<Zrakoplov>()))
                .ReturnsAsync(1);

            _repoMock
                .Setup(r => r.DeleteZrakoplovAsync(1))
                .ReturnsAsync(1);
            _repoMock
                .Setup(r => r.DeleteZrakoplovAsync(It.Is<int>(i => i != 1)))
                .ReturnsAsync(0);

            _service = new ZrakoplovService(_repoMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAll()
        {
            var result = await _service.GetAllAsync();
            Assert.IsNotNull(result);
            var arr = new List<ZrakoplovGetResponse>(result);
            Assert.AreEqual(1, arr.Count);
            Assert.AreEqual("Airbus A320", arr[0].Model);
            Assert.AreEqual("HR-A320", arr[0].Registracija);
        }

        [Test]
        public async Task GetByIdAsync_Existing_ReturnsResponse()
        {
            var resp = await _service.GetByIdAsync(1);
            Assert.IsNotNull(resp);
            Assert.AreEqual(1, resp.IdZrakoplova);
        }

        [Test]
        public async Task GetByIdAsync_NonExisting_ReturnsNull()
        {
            var resp = await _service.GetByIdAsync(42);
            Assert.IsNull(resp);
        }

        [Test]
        public async Task CreateAsync_ValidRequest_SetsIdAndReturnsResponse()
        {
            var req = new ZrakoplovCreateRequest {
                Model = "Test",
                Registracija = "IT-TEST1"
            };

            var resp = await _service.CreateAsync(req);
            Assert.IsNotNull(resp);
            Assert.AreEqual(99, resp.IdZrakoplova);
            _repoMock.Verify(r => r.CreateZrakoplovAsync(It.IsAny<Zrakoplov>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_Existing_ReturnsTrue()
        {
            var req = new ZrakoplovUpdateRequest {
                IdZrakoplova = 1,
                Model = "New",
                Registracija = "HR-NEW1"
            };

            var ok = await _service.UpdateAsync(req);
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.UpdateZrakoplovAsync(It.IsAny<Zrakoplov>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_NonExisting_ReturnsFalse()
        {
            var req = new ZrakoplovUpdateRequest {
                IdZrakoplova = 5,
                Model = "Nope",
                Registracija = "HR-NOPE"
            };

            var ok = await _service.UpdateAsync(req);
            Assert.IsFalse(ok);
        }

        [Test]
        public async Task DeleteAsync_Existing_ReturnsTrue()
        {
            var ok = await _service.DeleteAsync(1);
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.DeleteZrakoplovAsync(1), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_NonExisting_ReturnsFalse()
        {
            var ok = await _service.DeleteAsync(5);
            Assert.IsFalse(ok);
        }
    }