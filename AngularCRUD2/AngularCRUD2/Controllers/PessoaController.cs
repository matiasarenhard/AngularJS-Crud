using AngularCRUD2.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace AngularCRUD2.Areas.Controller
{
    public class PessoaController : ApiController
    {


        #region Buscar

        [HttpGet]
        public object BuscarPessoa()
        {
            using (BancoDadosEntities Obj = new BancoDadosEntities())
            {
                List<Pessoa> ListaPessoa = Obj.Pessoa.ToList();
                return ListaPessoa;
            }
        }
        #endregion


        #region Gravar
        [HttpPost]
        public string GravarPessoa(Pessoa Pessoa)
        {
            #region validações
            if (Pessoa.Email == "" || Pessoa.Email == null)
                return "Por favor informe o email";
            if (Pessoa.Telefone == "" || Pessoa.Telefone == null)
                return "Por favor informe o telefone";
            if (Pessoa.Nome == "" || Pessoa.Nome == null)
                return "Por favor informe o nome";
            if (Pessoa.Idade == 0 || Pessoa.Idade == null)
                return "Por favor informe a idade";
            #endregion

            if (Pessoa != null)
            {
                using (BancoDadosEntities Obj = new BancoDadosEntities())
                {
                    Obj.Pessoa.Add(Pessoa);
                    Obj.SaveChanges();
                    return "Pessoa adicionada com sucesso";
                }
            }
            else
            {
                return "Não foi possivel cadastrar está pessoa";
            }
        }
        #endregion

        #region Alterar
        [HttpPost]
        public string AlterarPessoa(Pessoa Pessoa)
        {
            #region validações
            if (Pessoa.Email == "" || Pessoa.Email == null)
                return "Por favor informe o email";
            if (Pessoa.Telefone == "" || Pessoa.Telefone == null)
                return "Por favor informe o telefone";
            if (Pessoa.Nome == "" || Pessoa.Nome == null)
                return "Por favor informe o nome";
            if (Pessoa.Idade == 0 || Pessoa.Idade == null)
                return "Por favor informe a idade";
            #endregion

            if (Pessoa != null)
            {
                using (BancoDadosEntities Con = new BancoDadosEntities())
                {
                    var pessoa = Con.Entry(Pessoa);
                    Pessoa Obj = Con.Pessoa.Where(x => x.IdPessoa == Pessoa.IdPessoa).FirstOrDefault();
                    if (Obj == null)
                        return "Está Pessoa não foi encontrada em nosso banco de dados, por favor contate o suporte.";

                    Obj.Email = Pessoa.Email;
                    Obj.Telefone = Pessoa.Telefone;
                    Obj.Nome = Pessoa.Nome;
                    Obj.Idade = Pessoa.Idade;
                    Con.SaveChanges();
                    return "Pessoa atualizada com sucesso!";
                }
            }
            else
            {
                return "Pessoa não atualizada";
            }
        }
        #endregion

        #region Deletar
        [HttpPost]
        public string DeletePessoa(Pessoa Pessoa)
        {
            if (Pessoa != null)
            {
                using (BancoDadosEntities Con = new BancoDadosEntities())
                {
                    var pessoa = Con.Entry(Pessoa);
                    if (pessoa.State == System.Data.Entity.EntityState.Detached)
                    {
                        Con.Pessoa.Attach(Pessoa);
                        Con.Pessoa.Remove(Pessoa);
                    }
                    Con.SaveChanges();
                    return "Pessoa deletada com sucesso";
                }
            }
            else
            {
                return "Pessoa não deletada";
            }
        }
        #endregion

    }
}