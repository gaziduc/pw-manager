using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PWManagerTests
{
    [TestClass]
    public class UnitTest1
    {
        long user_id;

        [TestInitialize]
        public void Init()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.GetUserFromCrdentialsAsync("unit_test", "unit_test").Result;
            if (res >= 0)
                user_id = res;
            else
                user_id = service.CreateUserAsync("unit_test", "unit_test").Result.Item1;


        }

        [TestMethod]
        public void GetService()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();
            
            var result = service.GetAllServiceCredentialsAsync(user_id);
            Assert.IsTrue(result.Result.Item1);
        }

        [TestMethod]
        public void AddService()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var result = service.CreateServiceAsync("test", "test", "test", "test", user_id, "Other");
            Assert.IsTrue(result.Result.Item1);
        }
        
        [TestMethod]
        public void AddServiceForNonExistantUser()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.CreateServiceAsync("test", "test", "test", "test", -42, "Other");
            Assert.IsFalse(res.Result.Item1);
        }

        [TestMethod]
        public void LoginExistsTest()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.LoginExistsAsync("unit_test");
            Assert.IsTrue(res.Result);
        }

        [TestMethod]
        public void LoginNotExistsTest()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.LoginExistsAsync("skdjczjhiuzeheiuhdiuhajkadnhkjadajkznjdakjndjknzdakjdnkjazddkjbkazdbjhzabhajzbdazh");
            Assert.IsFalse(res.Result);
        }


        [TestMethod]
        public void DeleteNonExistantServiceTest()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.DeleteServiceAsync(-42);
            Assert.IsFalse(res.Result);
        }

        [TestMethod]
        public void GetLoginTest()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.GetAllUsersLoginAsync().Result;
            Assert.IsTrue(res.Contains("unit_test"));
        }

        [TestMethod]
        public void GetLoginFailedTest()
        {
            MainService.MainServiceClient service = new MainService.MainServiceClient();

            var res = service.GetAllUsersLoginAsync().Result;
            Assert.IsFalse(res.Contains("skfhizhdiuhzdiuhaidhiuahdiuhiuahidhaiuhdkjckjdsbckbqjhbcjhbchbsjknddcdau"));
        }
    }
}
