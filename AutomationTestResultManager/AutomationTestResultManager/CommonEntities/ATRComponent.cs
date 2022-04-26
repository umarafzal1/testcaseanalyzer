namespace AutomationTestResultManager.CommonEntities
{
    public class ATRComponent
    {
        public ATRComponent()
        {
            TestCases = new HashSet<ATRTestCase>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ATRTestCase> TestCases { get; set; }
    }
}
