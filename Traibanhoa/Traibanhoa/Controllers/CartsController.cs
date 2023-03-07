﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Traibanhoa.Modules.CartDetailModule.Interface;
using Traibanhoa.Modules.CartDetailModule.Request;
using Traibanhoa.Modules.CartModule.Interface;

namespace Traibanhoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;

        public CartsController(ICartService cartService, ICartDetailService cartDetailService)
        {
            _cartService = cartService;
            _cartDetailService = cartDetailService;
        }

        // GET: api/Carts/E7E1BD28-8979-4B6E-ADBC-458408E6BA41
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetCartDetailsByCustomerId([FromRoute] Guid customerId)
        {
            try
            {
                var cart = _cartService.GetCartByCustomerId(customerId);
                var cart_details = await _cartDetailService.GetCartDetailsByCartId(cart.CartId);
                return new JsonResult(new
                {
                    total_items_in_cart = cart_details.Count,
                    cart_details
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/v1/carts
        [HttpPut("update-cart")]
        public async Task<ActionResult> UpdateQuantityItemInCart([FromBody] UpdatedItemInCart updatedItemInCart)
        {
            try
            {
                if (updatedItemInCart.Quantity == 0)
                {
                    await _cartDetailService.DeleteItemInCart(updatedItemInCart.CartId, updatedItemInCart.ItemId, updatedItemInCart.TypeItem);
                    return new JsonResult(new
                    {
                        status = "success"
                    });
                }
                var result = await _cartDetailService.UpdateQuantityItemInCart(updatedItemInCart);
                return new JsonResult(new
                {
                    status = "success",
                    cart_id = updatedItemInCart.CartId,
                    item_id = updatedItemInCart.ItemId,
                    quantity = updatedItemInCart.Quantity,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/v1/carts
        [HttpDelete]
        public async Task<ActionResult> DeleteItemInCart([FromBody] DeletedItemInCart item)
        {
            try
            {
                await _cartDetailService.DeleteItemInCart(item.CartId, item.ItemId, item.TypeItem);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/v1/carts
        [HttpPost]
        public async Task<ActionResult> AddNewItemIntoCart([FromBody] InsertedItemIntoCart newItem)
        {
            try
            {
                var result = await _cartService.InsertNewItemIntoCart(newItem);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}