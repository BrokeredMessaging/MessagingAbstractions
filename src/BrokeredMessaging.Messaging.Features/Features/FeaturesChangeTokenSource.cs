namespace BrokeredMessaging.Messaging.Features
{
    public class FeaturesChangeTokenSource
    {
        public FeaturesChangeToken Token => new FeaturesChangeToken(this);

        public bool HasChanged { get; private set; }

        public void Changed() => HasChanged = true;
    }
}
