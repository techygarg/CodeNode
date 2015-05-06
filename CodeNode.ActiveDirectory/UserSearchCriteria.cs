namespace CodeNode.ActiveDirectory
{
    public class UserSearchCriteria
    {
        public string SearchValue { get; set; }
        public string GroupName { get; set; }
        public SearchOn Parameter { get; set; }
        public bool ExactMatch { get; set; }
    }
}