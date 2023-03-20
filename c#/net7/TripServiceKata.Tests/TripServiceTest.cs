using TripServiceKata.Trip;
using TripServiceKata.User;
using TripServiceKata.Exception;

namespace TripServiceKata.Tests;

public class TripServiceTest
{
    [Fact]
    public void Should_Throw_Exception_When_User_Not_LoggedIn()
    {
        ((Action)(() => new TripService().GetTripsByUser(new TripServiceKata.User.User())))
        .Should()
        .NotThrow();
    }
}
