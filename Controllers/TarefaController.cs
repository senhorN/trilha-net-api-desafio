using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }
#region {ID}
        
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            //RESOLVIDO
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            Tarefa tarefa = _context.Tarefas.Find(id);
            if(ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }
#endregion

#region Metodo get {ObterTodos}
        
        
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            //RESOLVIDO
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            List<Tarefa> tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }
#endregion 

#region Metodo get {ObterPorTitulo}
        
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            //RESOLVIDO
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            Tarefa  tarefa = _context.Tarefas.Where(x => x.Titulo.Equals(titulo)).FirstOrDefault();

            if(tarefa is null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }
#endregion

#region Metodo get {ObterPorData}
        
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            //RESOLVIDO
            List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            if (tarefas is null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }
#endregion 

#region Metodo get {ObterPorStatus}
        
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            //RESOLVIDO
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();
            if (tarefas is null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }
#endregion 

#region Metodo Post {Criar}
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            //RESOLVIDO
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }
#endregion

#region Metodo Put {id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            //RESOLVIDO
            Tarefa tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound();
            }
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok();
            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            
        }
#endregion

#region Metodo Delete {id}
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            //RESOLVIDO
            Tarefa tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound();
            }
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            
        }
#endregion
    }
}
