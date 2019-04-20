using Hubster.Direct.Models;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterConversationCustomer
    {
        ApiResponse<EstablishedConversationModel> Establish(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request);
    }
}