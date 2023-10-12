using Cytidel.Core.Entities;
using Shouldly;

namespace Cytidel.Tests.Unit.Core.Entities;

public class ValidationEmailTests
{
    private bool Act(string email) => User.IsValidEmail(email);
    [Fact]
    public void given_valid_email_should_be_true()
    {
        var email = "emailtest@test.com";
        var result = Act(email);
        //Assert
        result.ShouldBeTrue();
    }
    [Fact]
    public void given_invalid_email_should_be_false()
    {
        var email = "emailtest@test.c";
        var result = Act(email);
        //Assert
        result.ShouldBeFalse();
    }
    [Fact]
    public void given_empty_email_should_be_false()
    {
        var email = "";
        var result = Act(email);
        //Assert
        result.ShouldBeFalse();
    }
}
