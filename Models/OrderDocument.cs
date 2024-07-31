using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindOrdersAPI.Models
{
    public class OrderDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        public int OrderID { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentData { get; set; }
        public DateTime UploadedDate { get; set; }

        public OrderDocument(int OrderID, string DocumentName, byte[] DocumentData, DateTime UploadedDate)
        {
            this.OrderID = OrderID;
            this.DocumentName = DocumentName;
            this.DocumentData = DocumentData;
            this.UploadedDate = UploadedDate;
        }
    }

}
