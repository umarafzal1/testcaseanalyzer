using System.ComponentModel.DataAnnotations.Schema;

namespace AutomationTestResultManager.CommonEntities
{
    public class ATRTestCase
    {
        public ATRTestCase()
        {
            Features = new List<ATRFeature>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string TestedBy { get; set; }

        [NotMapped]

        public string RowColor { get; set; }
        public string TestStatus { get; set; }
        public DateTime TestDate { get; set; }
        [ForeignKey(nameof(MasterComponent))]
        public int? MasterComponentId { get; set; }
        public ATRComponent? MasterComponent { get; set; }
        public List<ATRFeature> Features { get; set; }
    }
}
