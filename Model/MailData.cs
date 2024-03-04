using System.ComponentModel;

namespace Library.Model
{
    public class MailData
    {
        [DisplayName("Địa chỉ email người nhận")]
        public string ReceiverEmail { get; set; } = string.Empty;
        [DisplayName("Tên người nhận")]
        public string ReceiverName { get; set; }= string.Empty;
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }=string.Empty;
        [DisplayName("Nội dung")]
        public string Body { get; set; } = string.Empty;
    }
}
