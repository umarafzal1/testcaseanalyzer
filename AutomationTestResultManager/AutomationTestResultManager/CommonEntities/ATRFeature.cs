using System.ComponentModel.DataAnnotations.Schema;

namespace AutomationTestResultManager.CommonEntities
{
    public class ATRFeature
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(TestedInCase))]
        public int? TestedInCaseId { get; set; }
        public ATRTestCase? TestedInCase { get; set; }
    }
}
