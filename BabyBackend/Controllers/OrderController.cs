﻿using BabyBackend.Models;
using BabyBackend.Models.Dto;
using BabyBackend.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
       

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }


        [HttpPost("order-create")]
        [Authorize]
        public async Task<ActionResult> createOrder(long price)
        {
            try
            {
                if(price <= 0)
                {
                    return BadRequest("enter a valid money ");
                }
                var orderId = await _orderServices.OrderCreate(price);
                return Ok(orderId);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            

        }


        [HttpPost("payment")]
        [Authorize]
        public  ActionResult Payment(RazorpayDto razorpay)
        {
            try
            {
                if(razorpay == null)
                {
                    return BadRequest("razorpay details must not null here");
                }
                var con = _orderServices.Payment(razorpay);
                return Ok(con);
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }


        [HttpPost("place-Order")]
        [Authorize]
        public async Task<ActionResult> PlaceOrder( OrderRequestDto orderRequests)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (orderRequests == null || jwtToken == null)
                {
                    return BadRequest();
                }
                await _orderServices.CreateOrder(jwtToken, orderRequests);
                return Ok();
            }
            catch( Exception e )
            {
                return StatusCode(500, e.Message);
            }
            
        }



        [HttpGet("get_order_details")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetOrderDetails ()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (jwtToken == null)
                {
                    return BadRequest();
                }
                return Ok(await _orderServices.GetOrderDtails(jwtToken));
             
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            
        }

        [HttpGet("total_revenue")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetTotalRevenue()
        {
            try
            {
                return Ok(await _orderServices.GetTotalRevenue());
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
            
        }

        [HttpGet("get-order-details-admin")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetOrderDetailsAdmin()
        {
            try
            {
                return Ok(await _orderServices.GetOrderDetailAdmin());
            }catch( Exception e)
            {
                return StatusCode(500, e.Message);
            }
            

        }

        [HttpGet("get-detailed-order")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetDetailedOrder(int orderId)
        {
            try
            {
                return Ok(await _orderServices.GetOrderDetailsById(orderId));
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPut("update-order-status")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> UpdateOrder(int orderId,[FromBody] OrderAdminViewDto orderAdminView)
        {
            try
            {
                var status = await _orderServices.UpdateOrder(orderId, orderAdminView);
                return Ok();

            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
