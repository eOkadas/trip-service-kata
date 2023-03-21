using TripServiceKata.Trip;
using TripServiceKata.Exception;

namespace TripServiceKata.Tests;

public class TripServiceTest
{
    [Fact]
    public void Should_Throw_Exception_When_User_Not_Logged()
    {
        User.User anonymousUser = new User.User();

        ((Action)(() => new TestableAnonymousUserTripService().GetTripsByUser(anonymousUser)))
                            .Should()
                            .Throw<UserNotLoggedInException>();
    }

    [Fact]
    public void Should_Return_Empty_Trip_List_When_Tested_User_Has_No_Friends()
    {
        var loggedUser = new User.User();

        var inputUserWithoutFriends = new User.User();

        var actualTrips = new TestableLoggedUserTripService(loggedUser)
                            .GetTripsByUser(inputUserWithoutFriends);

        actualTrips.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_Empty_Trip_List_When_Tested_User_Is_Not_Friend_With_Logged_User()
    {
        var loggedUser = new User.User();

        var inputUserNotFriendWithLoggedUser = new User.User();

        inputUserNotFriendWithLoggedUser.AddFriend(new User.User());

        var actualTrips = new TestableLoggedUserTripService(loggedUser)
                            .GetTripsByUser(inputUserNotFriendWithLoggedUser);

        actualTrips.Should().BeEmpty();
    }


    [Fact]
    public void Should_Return_Trip_List_When_TestedUser_Is_Friends_With_LoggedUser()
    {
        var loggedUser = new User.User();

        var inputUserFriendWithLoggedUser = new User.User();
        inputUserFriendWithLoggedUser.AddFriend(loggedUser);

        var actualTrips = new TestableLoggedUserTripService(loggedUser)
        .GetTripsByUser(inputUserFriendWithLoggedUser);

        actualTrips.Should().NotBeEmpty();
    }

    private class TestableAnonymousUserTripService : TripService
    {
        public override User.User GetLoggedUser() => null;
    }

    private class TestableLoggedUserTripService : TripService
    {
        User.User loggedUser { get; init; }

        public TestableLoggedUserTripService(User.User loggedUser)
        {
            this.loggedUser = loggedUser;
        }
        public override User.User GetLoggedUser()
        {
            return loggedUser;
        }

        public override List<Trip.Trip> FindTripsByUserInDAO(User.User user)
            => new List<Trip.Trip>() {new Trip.Trip(), new Trip.Trip()};
    }
}
