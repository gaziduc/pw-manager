using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PWManagerTests
{
    [TestClass]
    public class UnitTest1
    {
        MainService.MainServiceClient _service;
        long user_id;

        [TestInitialize]
        public async void Init()
        {
            _service = new MainService.MainServiceClient();
            var res = await _service.CreateUserAsync("unit_tests", "unit_tests");
            user_id = res.Item1;
        }

        [TestMethod]
        public async void GetNonExistantService()
        {
            var res = await _service.GetAllServiceCredentialsAsync(-42);
            Assert.IsFalse(res.Item1);
        }

        [TestMethod]
        public async void AddService()
        {
            var res = await _service.CreateServiceAsync("test", "test", "test", "test", user_id, "Other");
            Assert.IsTrue(res.Item1);
        }

        [TestMethod]
        public async void AddServiceForNonExistantUser()
        {
            var res = await _service.CreateServiceAsync("test", "test", "test", "test", -42, "Other");
            Assert.IsFalse(res.Item1);
        }

        [TestMethod]
        public async void LoginExistsTest()
        {
            bool res = await _service.LoginExistsAsync("unit_tests");
            Assert.IsTrue(res);
        }

        [TestMethod]
        public async void LoginNotExistsTest()
        {
            bool res = await _service.LoginExistsAsync("skdjczjhiuzeheiuhdiuhajkadnhkjadajkznjdakjndjknzdakjdnkjazddkjbkazdbjhzabhajzbdazh");
            Assert.IsFalse(res);
        }


        [TestMethod]
        public async void DeleteNonExistantServiceTest()
        {
            bool res = await _service.DeleteServiceAsync(-42);
            Assert.IsFalse(res);
        }
    }
}
