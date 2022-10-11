using AutogService.BLL.Facade;
using Domain;
using Microsoft.AspNetCore.Mvc;
using ReceivingApp.Facade;
using MessagingService.Send;
using MessagingService.Receive;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        public PrintController()
        {
           Receiver receiver = new Receiver();

        }

        // GET: api/<PrintController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Printed> list = new List<Printed>();
                list = FacadeService.ReadAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<PrintController>
        [HttpGet("InitPrinter")]
        public IActionResult InitPrinter()
        {
            try
            {
                Receiver.Start();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<PrintController>
        [HttpGet("StopPrinter")]
        public IActionResult StopPrinter()
        {
            try
            {
                Receiver.Stop();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<PrintController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Object response = FacadeService.Read(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }       
        }

        // POST api/<PrintController>
        [HttpPost]
        public IActionResult Post([FromBody] RequestToPrint value)
        {
            try { 
            //ResponseOK? response = FacadeReceiving.Print(value);
            /*if(print != null)
            {
                FacadeService.Save(value);
            }
            return Ok(print);   
             */

            Sender.SendDocumentToQueue(value);            
            return Ok();// (print);
            } catch (Exception ex)
            {
                return BadRequest();
            }     
        }
    
    }

}
