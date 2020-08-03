using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tandem.API.Data.dtos;

namespace Tandem.API.Integration.Test
{
    [TestClass]
    public class UserControllerTests : BaseTest
    {
        [TestMethod]
        public async Task GetUser_returns_Success()
        {
            await CreateUser(await GetRequestDto());
            var request = new HttpRequestMessage(HttpMethod.Get, "user?email=test@integration.com");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //Clear Data
            await DeleteUser();
        }

        [TestMethod]
        public async Task GetUser_returns_NotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "user?email=test@integration1.com");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetUser_returns_BadRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "user?email=integration.com");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task CreateUser_returns_BadRequest()
        {
            var response = await CreateUser(new CreateUserRequestDto());
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateUser_returns_Success()
        {
            var response = await CreateUser(await GetRequestDto());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //Clear Data
            await DeleteUser();
        }

        [TestMethod]
        public async Task CreateUser_returns_conflict()
        {
            var response = await CreateUser(await GetRequestDto());

            response = await CreateUser(await GetRequestDto());

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

            //Clear Data
            await DeleteUser();
        }

        private async Task<HttpResponseMessage> CreateUser(CreateUserRequestDto user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "user");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (user != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            }

            var response = await Client.SendAsync(request);
            return response;

        }

        private async Task<CreateUserRequestDto> GetRequestDto()
        {
            return new CreateUserRequestDto()
            {
                FirstName = "Test",
                LastName = "Integration",
                EmailAddress = "test@integration.com",
                PhoneNumber = "7683459870"
            };
        }

        private async Task DeleteUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "user?email=test@integration.com");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Client.SendAsync(request);
        }
    }
}
