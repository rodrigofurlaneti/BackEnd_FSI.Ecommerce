namespace FSI.Ecommerce.Domain.Entities
{
    public class IndividualProfile
    {
        public long AccountId { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string NationalId { get; private set; } = null!;

        public Account Account { get; private set; } = null!;

        private IndividualProfile() { }

        public IndividualProfile(long accountId, string firstName, string lastName, string nationalId)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
        }
    }
}