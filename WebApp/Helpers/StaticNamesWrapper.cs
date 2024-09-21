using devops_project.Interfaces;

namespace devops_project.Helpers
{
    public class StaticNamesWrapper : IStaticNamesWrapper
    {
        private static string[] Names = 
            {
                "João",
                "Maria",
                "Pedro",
                "Ana",
                "Lucas",
                "Fernanda",
                "Rafael",
                "Camila",
                "Gabriel",
                "Juliana",
                "Matheus",
                "Larissa",
                "Felipe",
                "Beatriz",
                "Thiago",
                "Mariana",
                "Bruno",
                "Gabriela",
                "Daniel",
                "Isabel",
                "Eduardo",
                "Luana",
                "Rodrigo",
                "Renata",
                "Vinícius",
                "Carolina",
                "Leonardo",
                "Amanda",
                "Gustavo",
                "Aline",
                "André",
                "Patrícia",
                "Carlos",
                "Vitória",
                "Diego",
                "Rafaela",
                "José",
                "Letícia",
                "Antônio",
                "Bianca",
                "Marcelo",
                "Natália",
                "Alexandre",
                "Vanessa",
                "Henrique",
                "Sofia",
                "Francisco",
                "Yasmin",
                "Roberto",
                "Clara"
            };

        public string[] GetNamesList()
        {
            return Names;
        }
    }
}
