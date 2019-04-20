namespace Hubster.Direct.Interfaces
{
    public interface IHubsterDirectClientBusiness
    {
        IHubsterActivityBusiness Activity { get; }
        IHubsterConversationBusiness Conversation { get; }
        IHubsterEventsBusiness Events { get; }
    }
}