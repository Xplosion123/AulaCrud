using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using AulaCrud.Models;
using AulaCrud.Data;
namespace AulaCrud.Controllers
{
    public class ContatosController : Controller
    {
        private readonly DataBase db = new DataBase();
        public IActionResult Index()
        {
            var contatos = new List<Contatos>();

            using var conn = db.GetConnection();
            using var cmd = new MySqlCommand("sp_contato_listar", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                contatos.Add(new Contatos
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Tel = reader.GetString("telefone"),
                    Email = reader.GetString("email")
                });
            }

            return View(contatos);
        }
        public IActionResult CadastrarContato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarContato(Contatos Cont)
        {
            using var conn = db.GetConnection();
            using var cmd = new MySqlCommand("sp_contato_criar", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_nome", Cont.Nome);
            cmd.Parameters.AddWithValue("p_tel", Cont.Tel);
            cmd.Parameters.AddWithValue("p_email", Cont.Email);
            cmd.ExecuteNonQuery();

            TempData["Mensagem"] = "Contato cadastrado com sucesso!";

            return RedirectToAction("Index");
        }
        // GET: Contatos/Edit/5
        public IActionResult Edit(int id)
        {
            Contatos contato = null;

            using var conn = db.GetConnection();
            using var cmd = new MySqlCommand("sp_contato_obter", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                contato = new Contatos
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Tel = reader.GetString("telefone"),
                    Email = reader.GetString("email")
                };
            }

            if (contato == null)
                return NotFound();

            return View(contato);
        }

        // POST: Contatos/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Contatos Cont)
        {
            using var conn = db.GetConnection();
            using var cmd = new MySqlCommand("sp_contato_editar", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id", id);
            cmd.Parameters.AddWithValue("p_nome", Cont.Nome);
            cmd.Parameters.AddWithValue("p_tel", Cont.Tel);
            cmd.Parameters.AddWithValue("p_email", Cont.Email);
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            using var conn = db.GetConnection();
            using var cmd = new MySqlCommand("sp_contato_excluir", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id", id);
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
    }
}

