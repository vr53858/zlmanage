namespace ZLManage.DomainModel.Models.Requests;

public class LetUpdateRequest
{
    public int BrojLeta { get; set; }
    public DateTime? DatumPolaska { get; set; }
    public DateTime? DatumDolaska { get; set; }
    public int? IdZrakoplova { get; set; }
    public int? IdPiste { get; set; }
    public int? KreiraoGa { get; set; }
}