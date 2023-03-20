using TripServiceKata.Trip;
using TripServiceKata.User;
using TripServiceKata.Exception;

namespace TripServiceKata.Tests;

public class TripServiceTest
{
    [Fact]
    public void Should_Throw_Exception_When_User_Not_Logged()
    {
        var loggedUser = new User.User();
        
        ((Action)(() => new TestableTripService(loggedUser).GetTripsByUser(loggedUser)))
        .Should()
        .Throw<UserNotLoggedInException>();
    }

    private class TestableTripService: TripService
    {
        User.User MockInputUser {get; init;}

        public TestableTripService(User.User mockInputUser)
        {
            MockInputUser = mockInputUser;    
        }

        public override User.User GetLoggedUser() => MockInputUser;
    }
}
