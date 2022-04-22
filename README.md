# InventoryApplication
Basic Example of Creating Unit Tests

# Exercise  1
Your task is to add tests for the function InventoryService.TryAddItem.

In order for the function to be testable, some changes will have to be introduced.

While making changes, you must ensure to change as little as possible while still ensuring that the function becomes fully testable, without relying on external dependencies, such as other classes.

You're not allowed to change the Program.cs-class inside InventoryApplication at all - This represents an outside consumer of your class, that must not be affected by your changes.

Once changes are in place, add test-cases for all possible paths through the function.

# Exercise  2
Your task is to add tests for the function RevenueService.CalculateRevenue.

As above, this will first require some changes in order to make the function testable without relying on external dependencies.

Once changes are in place, add test-cases for all possible paths through the function.

# Exercise  3
Your task is to add tests for the function MarketService.GetByClosestLocation.

The test must not rely on an actual database and hence will require some changes in order to be testable.

Once changes are in place, add the following test-cases:
- There are no MarketPrices to be found.
- There are multiple MarketPrices to be found - Ensure that the closest is returned.
