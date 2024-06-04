namespace VotingSystem.Core.Test
{
    public class VotingPollFactoryTests
    {
        private readonly VotingPollFactory _factory;
        private readonly VotingPollRequest _request = new VotingPollRequest
        {
            Names = new string[] { "option1", "option2" },
            Title = "",
            Description = ""
        };

        public VotingPollFactoryTests()
        {
            _factory = new VotingPollFactory();
        }

        [Fact]
        public void Create_Poll_Should_Throw_When_Less_Than_Two_Counters_Name()
        {
            //Arrange
            _request.Names = new[] { "option1" };
            
            //Act
            Action action = () => _factory.Create(_request);
            _request.Names = new string[] { };
            Action action2 = () => _factory.Create(_request);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage("Create poll required atleaset two names.");
            action2.Should().Throw<ArgumentException>().WithMessage("Create poll required atleaset two names.");
        }

        [Fact]
        public void Create_Poll_Should_Provide_VotingCounter_For_Each_Provided_Name()
        {
            //Act
            var result = _factory.Create(_request);

            //Assert
            result.Counters.Should().HaveCount(_request.Names.Length);
            foreach (var name in _request.Names)
            {
                result.Counters.Should().Contain(c => c.VotingOptionName == name);
            }
        }

        [Fact]
        public void Create_Add_Titles_To_Poll()
        {
            //Act
            var poll = _factory.Create(_request);

            //Asset
            poll.Title.Should().Be(_request.Title);
        }

        [Fact]
        public void Create_Add_Description_To_Poll()
        {
            //Arrange
            _request.Title= "test";
            _request.Description = "test description";

            //Act
            var poll = _factory.Create(_request);

            //Asset
            poll.Description.Should().Be(_request.Description);
        }
    }
}
