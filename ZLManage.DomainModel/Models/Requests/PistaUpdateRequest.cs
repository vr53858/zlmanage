namespace ZLManage.DomainModel.Models.Requests;

public class PistaUpdateRequest
{
    public int IdPiste { get; set; }
    public int? Duljina { get; set; }
    public string Oznaka { get; set; }
}