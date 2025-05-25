namespace ZLManage.DomainModel.Models.Requests;

public class LetCreateRequest
{
    public DateTime? DatumPolaska { get; set; }
    public DateTime? DatumDolaska { get; set; }
    public int? IdZrakoplova { get; set; }
    public int? IdPiste { get; set; }
    public int? KreiraoGa { get; set; }
}