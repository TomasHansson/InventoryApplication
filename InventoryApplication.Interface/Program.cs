// See https://aka.ms/new-console-template for more information

using InventoryApplication;

// Lets pretend this is actually a fancy Web Application, that sends in a request to add the new inventory.

var inventoryService = new InventoryService();
var success = inventoryService.TryAddItem("Hammer", "Strong Hammer!", "Tools", 9.99, DateTime.Today.AddDays(3));
Console.WriteLine(success ? "Item was added" : "Item failed to be added.");
