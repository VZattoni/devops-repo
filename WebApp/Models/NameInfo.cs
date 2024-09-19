namespace devops_project.Models
{
    public class NameInfo
    {
        public string? Nome { get; set; }
        public string? Localidade { get; set; }
        public string? Sexo { get; set; }
        public List<ResInfo> Res { get; set; }
    }
}
