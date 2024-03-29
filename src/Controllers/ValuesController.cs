﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Get api/values/
        [HttpGet]
        public ActionResult Eval(){
            var engine = new Engine.RuleEngine();
            var result = engine.Execute(new ExpressionRequest()
            {
                Context = "customer",
                Payload = "{\"Name\": \"Moses\",\"Age\": 17,\"Gender\": \"E\"}"
            });
            
            return Ok(new
            {
                MessageBody = result.Reason,
                Result = result.IsThreeD
            });
        }

        // POST api/values/
        [HttpPost]
        public ActionResult Eval([FromBody] ExpressionRequest request){
            var engine = new Engine.RuleEngine();
            var result = engine.Execute(request);
            return Ok(new
            {
                MessageBody = result.Reason,
                Result = result.IsThreeD
            });
        }

        public class ExpressionRequest
        {
            public Guid CustomerId { get; set; }
            public string Context { get; set; }
            public string Payload { get; set; }
        }
    }


    class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }

        public bool IsCustomerGreatherThan(){
            return Age > 29;
        }
    }
}