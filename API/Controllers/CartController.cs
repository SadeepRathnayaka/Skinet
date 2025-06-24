using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
    {
        var carts = await cartService.GetCartAsync(id);

        return Ok(carts ?? new ShoppingCart { id = id });
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var updateCart = await cartService.SetCartAsync(cart);

        if (updateCart == null) return BadRequest("Problem with Cart");

        return updateCart;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var result = await cartService.DeleteCartAsync(id);

        if (!result) return BadRequest("Problem deleting cart");

        return Ok();
    }
}
