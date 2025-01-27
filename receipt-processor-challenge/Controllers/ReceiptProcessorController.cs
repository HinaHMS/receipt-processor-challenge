using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using receipt_processor_challenge.Models;

namespace receipt_processor_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptProcessorController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, ReceiptData> receipts = new();

        [HttpPost("process")]
        public IActionResult ProcessReceipt([FromBody] Receipt receipt)
        {
            var id = Guid.NewGuid().ToString();
            var points = CalculatePoints(receipt);
            receipts[id] = new ReceiptData(receipt, points);
            return Ok(new { id });
        }

        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id)
        {
            if (receipts.TryGetValue(id, out var receiptData))
            {
                return Ok(new { points = receiptData.Points });
            }
            return NotFound(new { error = "No receipt found for that ID." });
        }

        private int CalculatePoints(Receipt receipt)
        {
            int points = 0;
            points += receipt.Retailer.Count(char.IsLetterOrDigit);
            if (decimal.TryParse(receipt.Total, out var total))
            {
                if (total % 1 == 0) points += 50;
                if (total % 0.25m == 0) points += 25;
            }
            points += (receipt.Items.Count / 2) * 5;
            foreach (var item in receipt.Items)
            {
                var trimmedLength = item.ShortDescription.Trim().Length;
                if (trimmedLength % 3 == 0 && decimal.TryParse(item.Price, out var price))
                {
                    points += (int)Math.Ceiling(price * 0.2m);
                }
            }
            if (DateTime.TryParse(receipt.PurchaseDate, out var date) && date.Day % 2 == 1)
                points += 6;
            if (TimeSpan.TryParse(receipt.PurchaseTime, out var time) && time.Hours >= 14 && time.Hours < 16)
                points += 10;
            return points;
        }
    }
}