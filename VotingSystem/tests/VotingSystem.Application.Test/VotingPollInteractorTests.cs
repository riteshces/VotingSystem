namespace VotingSystem.Application.Test
{
    public class VotingPollInteractorTests
    {
        private readonly VotingPollRequest _request;
        private readonly Mock<IVotingPollFactory> _mockFactory;
        private readonly Mock<IVotingSystemPersistance> _mockPersistance;
        private readonly VotingPollInteractor _interactor;

        public VotingPollInteractorTests()
        {
            _request = new VotingPollRequest();
            _mockFactory = new Mock<IVotingPollFactory>();
            _mockPersistance = new Mock<IVotingSystemPersistance>();
            _interactor = new VotingPollInteractor(_mockFactory.Object, _mockPersistance.Object);
        }

        [Fact]
        public void CreateVotingPoll_Use_VotingPollFactory_To_Create_VotingPoll()
        {
            //Act
            _interactor.CreateVotingPoll(_request);

            //Assert
            _mockFactory.Verify(x => x.Create(_request));
        }

        [Fact]
        public void CreateVotingPoll_PersistsCreatedPoll()
        {
            //Arrange
            var poll = new VotingPoll();
            _mockFactory.Setup(x => x.Create(_request)).Returns(poll);

            //Act
            _interactor.CreateVotingPoll(_request);

            //Assert
            _mockPersistance.Verify(x => x.SaveVotingPoll(poll));
        }
    }
}
