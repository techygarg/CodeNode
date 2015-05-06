namespace CodeNode.ActiveDirectory
{
    public enum SearchOn
    {
        Description = 1,
        Guid = 2,
        Name = 3,
        SamAccountName = 4,
        //Sid should be System.Security.IdentityReference
        Sid = 5,
        UserPricipalName = 6,
        Email = 7,
        Firstname = 8,
        MiddleName = 9,
        SurName = 10
    }
}