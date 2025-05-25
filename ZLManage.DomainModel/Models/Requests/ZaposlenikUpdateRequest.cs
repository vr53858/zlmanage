namespace ZLManage.DomainModel.Models.Requests;

public class ZaposlenikUpdateRequest
{
    public int IdZaposlenika { get; set; }
    public string Pozicija { get; set; }
    public int? KreiraoGaAdmin { get; set; }
}