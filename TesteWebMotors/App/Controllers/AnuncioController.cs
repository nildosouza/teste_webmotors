using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dados;
using System.Data;
using App.Models;
using ApiAcesso;
using Newtonsoft.Json;

namespace App.Controllers
{
    public class AnuncioController : Controller
    {                
        private DadosAcesso _dadosAcesso = new DadosAcesso(Conexao.GetConnection(ConfigurationManager.ConnectionStrings["teste_webmotors"].ConnectionString));
        private ClienteApi _clienteApi = new ClienteApi("http://desafioonline.webmotors.com.br/api/OnlineChallenge/");

        // GET: Anuncio
        public ActionResult Index()
        {

            string strQuery = "select * from tb_AnuncioWebMotors";

            DataTable dt = _dadosAcesso.ExecuteQuery(strQuery);

            List<Anuncio> anuncios = new List<Anuncio>();

            foreach (DataRow row in dt.Rows)
            {
                Anuncio anuncio = new Anuncio();

                anuncio.ID = Convert.ToInt32(row["ID"]);
                anuncio.Marca = row["marca"].ToString();
                anuncio.Modelo = row["modelo"].ToString();
                anuncio.Versao = row["versao"].ToString();
                anuncio.Ano = Convert.ToInt32(row["ano"]);
                anuncio.Quilometragem = Convert.ToInt32(row["quilometragem"]);
                anuncio.Observacao = row["observacao"].ToString();

                anuncios.Add(anuncio);
            }

            return View(anuncios);
        }

        // GET: Anuncio/Details/5
        public ActionResult Details(int id)
        {
            string strQuery = $"select * from tb_AnuncioWebMotors where id = {id}";

            DataTable dt = _dadosAcesso.ExecuteQuery(strQuery);

            Anuncio anuncio = new Anuncio();

            foreach (DataRow row in dt.Rows)
            {
                anuncio.ID = Convert.ToInt32(row["ID"]);
                anuncio.Marca = row["marca"].ToString();
                anuncio.Modelo = row["modelo"].ToString();
                anuncio.Versao = row["versao"].ToString();
                anuncio.Ano = Convert.ToInt32(row["ano"]);
                anuncio.Quilometragem = Convert.ToInt32(row["quilometragem"]);
                anuncio.Observacao = row["observacao"].ToString();
            }

            return View(anuncio);
        }

        // GET: Anuncio/Create
        public ActionResult Create()
        {            
            string strRetorno = _clienteApi.getDados("Make");            

            List <Marca> marcas =  JsonConvert.DeserializeObject<Marca[]>(strRetorno).ToList();

            SelectList dropDown = new SelectList(marcas, "ID", "Name");            
            ViewBag.ListaMarca = dropDown;

            ViewBag.ListaModelo = new SelectList(new List<Modelo>(), "ID", "Name");
            ViewBag.ListaVersao = new SelectList(new List<Versao>(), "ID", "Name"); ;

            return View();
        }

        // POST: Anuncio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {                
                string strQuery = $"insert into tb_AnuncioWebMotors (marca,modelo,versao,ano,quilometragem,observacao)" +
                                  $"values('{collection["marca"]}','{collection["modelo"]}','{collection["versao"]}'," +
                                  $"{collection["ano"]},{collection["Quilometragem"]},'{collection["Observacao"]}')";

                _dadosAcesso.ExecuteNonQuery(strQuery);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Anuncio/Edit/5
        public ActionResult Edit(int id)
        {
            string strQuery = $"select * from tb_AnuncioWebMotors where id = {id}";

            DataTable dt = _dadosAcesso.ExecuteQuery(strQuery);
            
            Anuncio anuncio = new Anuncio();
            
            foreach (DataRow row in dt.Rows)
            {               
                anuncio.ID = Convert.ToInt32(row["ID"]);
                anuncio.Marca = row["marca"].ToString();
                anuncio.Modelo = row["modelo"].ToString();
                anuncio.Versao = row["versao"].ToString();
                anuncio.Ano = Convert.ToInt32(row["ano"]);
                anuncio.Quilometragem = Convert.ToInt32(row["quilometragem"]);
                anuncio.Observacao = row["observacao"].ToString();                
            }

            return View(anuncio);
        }

        // POST: Anuncio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string strQuery = $"update tb_AnuncioWebMotors set " +
                    $"marca = '{collection["marca"]}', " +
                    $"modelo = '{collection["modelo"]}', " +
                    $"versao = '{collection["versao"]}', " +                    
                    $"observacao = '{collection["observacao"]}', " +
                    $"ano = {collection["ano"]}, " +
                    $"quilometragem = {collection["quilometragem"]} " +
                    $"where id = {id}";

                _dadosAcesso.ExecuteNonQuery(strQuery);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Anuncio/Delete/5
        public ActionResult Delete(int id)
        {
            string strQuery = $"select * from tb_AnuncioWebMotors where id = {id}";

            DataTable dt = _dadosAcesso.ExecuteQuery(strQuery);

            Anuncio anuncio = new Anuncio();

            foreach (DataRow row in dt.Rows)
            {
                anuncio.ID = Convert.ToInt32(row["ID"]);
                anuncio.Marca = row["marca"].ToString();
                anuncio.Modelo = row["modelo"].ToString();
                anuncio.Versao = row["versao"].ToString();
                anuncio.Ano = Convert.ToInt32(row["ano"]);
                anuncio.Quilometragem = Convert.ToInt32(row["quilometragem"]);
                anuncio.Observacao = row["observacao"].ToString();
            }

            return View(anuncio);            
        }

        // POST: Anuncio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string strQuery = $"delete from tb_AnuncioWebMotors where id = {id}";
                _dadosAcesso.ExecuteNonQuery(strQuery);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Consulta()
        {
            string strRetorno = _clienteApi.getDados("Make");

            List<Marca> marcas = JsonConvert.DeserializeObject<Marca[]>(strRetorno).ToList();

            SelectList dropDown = new SelectList(marcas, "ID", "Name");
            ViewBag.ListaMarca = dropDown;

            ViewBag.ListaModelo = new SelectList(new List<Modelo>(), "ID", "Name");
            ViewBag.ListaVersao = new SelectList(new List<Versao>(), "ID", "Name"); ;

            return View();
        }

        [HttpPost]
        public ActionResult GetModelo(string marcaId)
        {            
            string strRetorno = _clienteApi.getDados($"Model?MakeID={marcaId}");

            List<Modelo> modelos = JsonConvert.DeserializeObject<Modelo[]>(strRetorno).ToList();

            SelectList dropDown = new SelectList(modelos, "ID", "Name");            

            return Json(dropDown);
        }

        [HttpPost]
        public ActionResult GetVersao(string modeloId)
        {
            string strRetorno = _clienteApi.getDados($"Version?ModelID={modeloId}");

            List<Versao> versaos = JsonConvert.DeserializeObject<Versao[]>(strRetorno).ToList();

            SelectList dropDown = new SelectList(versaos, "ID", "Name");

            return Json(dropDown);
        }
    }
}
