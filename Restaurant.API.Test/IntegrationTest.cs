namespace Restaurant.API.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.API.Models;
using Restaurant.API.DTO;
using Restaurant.API.Repository;
using Restaurant.API.Services;
using Moq;

public class TokenJson {
    public string? token { get; set; }
}
public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient _client;
    public Mock<IUserRepository> _mockUserRepository;
    public Mock<IReservationRepository> _mockReservationRepository;
    public IntegrationTest(WebApplicationFactory<Program> factory)
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockReservationRepository = new Mock<IReservationRepository>();

        _client = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IUserRepository));
                if (descriptor != null) services.Remove(descriptor);
                services.AddSingleton(_mockUserRepository.Object);

                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IReservationRepository));
                if (descriptor != null) services.Remove(descriptor);
                services.AddSingleton(_mockReservationRepository.Object);
            });
        }).CreateClient();
    }

[Theory(DisplayName = "Testando a rota /POST User")]
[InlineData("/user")]
public async Task TestPostUser(string url)
{
    // Arrange

    User userMoq = new User { Guid = "aaa", Name = "Rebeca", Email = "rebeca@betrybe.com", Password = "senha123"};
    _mockUserRepository.Setup(r => r.Signup(It.IsAny<User>())).Returns(userMoq);

    // Act

    var inputObj = new {
        Name = "Rebeca",
        Email = "rebeca@betrybe.com",
        Password = "senha123"
    };

    var response = await _client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(inputObj), System.Text.Encoding.UTF8, "application/json"));
    var responseString = await response.Content.ReadAsStringAsync();
    TokenJson jsonResponse = JsonConvert.DeserializeObject<TokenJson>(responseString)!;

    // Assert

    Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
    Assert.NotEmpty(jsonResponse.token);
}


[Theory(DisplayName = "Testando a rota /POST Login")]
[InlineData("/user/login")]
public async Task TestPostLogin(string url)
{
    // Arrange

    User userMoq = new User { Guid = "aaa", Name = "Rebeca", Email = "rebeca@betrybe.com", Password = "senha123"};
    _mockUserRepository.Setup(r => r.Login(It.IsAny<LoginDto>())).Returns(userMoq);

    // Act

    var inputObj = new {
        Email = "rebeca@betrybe.com",
        Password = "senha123"
    };

    var response = await _client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(inputObj), System.Text.Encoding.UTF8, "application/json"));
    var responseString = await response.Content.ReadAsStringAsync();
    TokenJson jsonResponse = JsonConvert.DeserializeObject<TokenJson>(responseString)!;

    // Assert

    Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
    Assert.NotEmpty(jsonResponse.token);
}

[Theory(DisplayName = "Testando a rota /POST Reservations")]
[InlineData("/reservations")]
public async Task TestPostReservation(string url)
{
    
    // Arrange

    User userMoq = new User { Guid = "aaa", Name = "Rebeca", Email = "rebeca@betrybe.com", Password = "senha123"};
    _mockUserRepository.Setup(r => r.GetUser(It.IsAny<string>())).Returns(userMoq);

    Reservation reservationMoq = new Reservation { User = "aaa", Date = new DateTime(2023,10,10), GuestQuant = 2 };
    _mockReservationRepository.Setup(r => r.Create(It.IsAny<Reservation>())).Returns(reservationMoq);

    // Act

    var inputObj = new {
        Date = "2023-10-10",
        GuestQuant = "3"
    };

    var token = new TokenGenerator().Generate(userMoq);
    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    var response = await _client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(inputObj), System.Text.Encoding.UTF8, "application/json"));
    var responseString = await response.Content.ReadAsStringAsync();
    Reservation jsonResponse = JsonConvert.DeserializeObject<Reservation>(responseString)!;

    // Assert

    Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
    Assert.Equal("aaa", jsonResponse.User);

}

[Theory(DisplayName = "Testando a rota /POST Reservations")]
[InlineData("/reservations")]
public async Task TestPostReservationUnathorized(string url)
{
    
    // Arrange
    // Act

    var inputObj = new {
        Date = "2023-10-10",
        GuestQuant = "3"
    };

    var response = await _client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(inputObj), System.Text.Encoding.UTF8, "application/json"));

    // Assert
    
    Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response?.StatusCode);

}

[Theory(DisplayName = "Testando a rota /GET Reservations")]
[InlineData("/reservations/2023-10-10")]
public async Task TestGetReservation(string url)
{
    // Arrange

    List<Reservation> reservationsMoq = new List<Reservation> { new Reservation { User = "aaa", Date = new DateTime(2023,10,10), GuestQuant = 2 }};
    _mockReservationRepository.Setup(r => r.Get(It.IsAny<DateTime>())).Returns(reservationsMoq);

    // Act

    var response = await _client.GetAsync(url);
    var responseString = await response.Content.ReadAsStringAsync();
    List<Reservation> jsonResponse = JsonConvert.DeserializeObject<List<Reservation>>(responseString)!;

    // Assert

    Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
    Assert.Equal("aaa", jsonResponse[0].User);

}

}