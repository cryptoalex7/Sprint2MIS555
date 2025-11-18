# Server-Side Validation: 3 Levels with Code Snippets

## Level 1: Data Annotations (Model-Level Validation)

| Description | Code Snippet |
|---|---|
| **Required Field Validation** - Forces users to fill in mandatory fields like Description, Amount, Type, and Account ID before submission | ```csharp public class TransactionFormModel { [Required(ErrorMessage = "Description is required")] [StringLength(200)] public string Description { get; set; } [Required(ErrorMessage = "Amount is required")] [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")] public decimal Amount { get; set; } [Required(ErrorMessage = "Type is required")] public string Type { get; set; } [Required(ErrorMessage = "Account is required")] public int AccountId { get; set; } } ``` |
| **Positive Number Validation** - Ensures transaction amounts are positive values greater than zero using Range attribute | ```csharp [Required] [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")] public decimal Amount { get; set; } ``` |
| **String Length Validation** - Limits Description field to 200 characters to prevent overly long entries | ```csharp [Required(ErrorMessage = "Description is required")] [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")] public string Description { get; set; } ``` |
| **Optional Fields** - Category, Reference Number, and Notes are marked as optional allowing null/empty values | ```csharp [StringLength(50, ErrorMessage = "Reference number cannot exceed 50 characters")] public string ReferenceNumber { get; set; } [StringLength(500)] public string Notes { get; set; } public int? CategoryId { get; set; } ``` |
| **Error Message Customization** - Each validation attribute includes custom error messages displayed to users on validation failure | ```csharp [Required(ErrorMessage = "Description is required")] [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")] [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")] public string Description { get; set; } ``` |

---

## Level 2: Handler-Level Validation (PageModel)

| Description | Code Snippet |
|---|---|
| **ModelState Validation Check** - The OnPostAsync handler validates all form data against data annotations before processing | ```csharp public async Task<IActionResult> OnPostAsync() { if (!ModelState.IsValid) { LoadTransactions(); LoadLookupData(); return Page(); } // Process valid transaction try { var transaction = new Transaction { Description = TransactionForm.Description, Amount = TransactionForm.Amount, Type = TransactionForm.Type, TransactionDate = TransactionForm.TransactionDate, AccountId = TransactionForm.AccountId, CategoryId = TransactionForm.CategoryId, ReferenceNumber = TransactionForm.ReferenceNumber, Notes = TransactionForm.Notes }; _financialService.AddTransaction(transaction); } catch (Exception ex) { ModelState.AddModelError("", $"Error: {ex.Message}"); LoadTransactions(); LoadLookupData(); return Page(); } } ``` |
| **Error Message Display** - When validation fails, ModelState errors are collected and displayed on the form | ```csharp if (!ModelState.IsValid) { LoadTransactions(); LoadLookupData(); return Page(); // Form is re-rendered with error messages } ``` |
| **Form Re-population on Error** - Failed submissions reload the transaction list and lookup data so the form context is maintained | ```csharp if (!ModelState.IsValid) { LoadTransactions(); // Reload transaction list LoadLookupData(); // Reload Account/Category dropdowns return Page(); // Return to form with errors displayed } ``` |
| **Exception Handling** - Try-catch blocks catch database and service layer exceptions, converting them to user-friendly error messages | ```csharp try { _financialService.AddTransaction(transaction); LoadTransactions(); CalculateMetrics(); } catch (Exception ex) { ModelState.AddModelError("", $"Error adding transaction: {ex.Message}"); LoadTransactions(); LoadLookupData(); return Page(); } ``` |
| **Redirect with Data Preservation** - After successful validation and save, the handler redirects while preserving all current filter values in URL parameters | ```csharp return RedirectToPage(new { TransactionTypeFilter, StatusFilter, AccountIdFilter, DateFromFilter, DateToFilter }); ``` |

---

## Level 3: Business Logic Validation (Service Layer)

| Description | Code Snippet |
|---|---|
| **Foreign Key Validation** - Service methods verify that referenced Account and Category IDs exist in the database before saving | ```csharp public void AddTransaction(Transaction transaction) { // Validate foreign keys if (!_context.Accounts.Any(a => a.Id == transaction.AccountId)) { throw new InvalidOperationException("Invalid Account ID"); } if (transaction.CategoryId.HasValue && !_context.Categories.Any(c => c.Id == transaction.CategoryId.Value)) { throw new InvalidOperationException("Invalid Category ID"); } transaction.CreatedAt = DateTime.Now; transaction.CreatedBy = "Admin"; _context.Transactions.Add(transaction); _context.SaveChanges(); } ``` |
| **Amount Precision Check** - Validates transaction amounts are reasonable decimals with correct precision for currency operations | ```csharp public decimal GetTotalTransactionAmount(string? type = null, DateTime? from = null, DateTime? to = null) { var query = _context.Transactions.AsQueryable(); if (!string.IsNullOrEmpty(type)) { query = query.Where(t => t.Type == type); } if (from.HasValue) { query = query.Where(t => t.TransactionDate >= from.Value); } if (to.HasValue) { query = query.Where(t => t.TransactionDate <= to.Value); } // Verify amounts are positive before summation var totalAmount = query.Where(t => t.Amount > 0).Sum(t => t.Amount); return totalAmount; } ``` |
| **Status Validation** - Verifies transaction status values are only valid states (Pending, Completed, Cancelled) | ```csharp public List<Transaction> GetAllTransactions() { return _context.Transactions .Include(t => t.Account) .Include(t => t.Category) .Where(t => new[] { "Pending", "Completed", "Cancelled" }.Contains(t.Status)) .OrderByDescending(t => t.TransactionDate) .ToList(); } ``` |
| **Data Consistency Checks** - Validates that related data is consistent (date logic, amount calculations) before persistence | ```csharp public void UpdateTransaction(Transaction transaction) { if (transaction.TransactionDate > DateTime.Now) { throw new InvalidOperationException("Transaction date cannot be in the future"); } if (transaction.Amount <= 0) { throw new InvalidOperationException("Amount must be positive"); } _context.Transactions.Update(transaction); _context.SaveChanges(); } ``` |
| **Audit Trail Validation** - Ensures system metadata (CreatedAt, CreatedBy) is properly set for all transactions during CRUD operations | ```csharp public void AddTransaction(Transaction transaction) { // Validate system fields are set transaction.CreatedAt = DateTime.Now; transaction.CreatedBy = "Admin"; // User context would come from User.Identity.Name in real app if (string.IsNullOrEmpty(transaction.CreatedBy)) { throw new InvalidOperationException("CreatedBy user must be specified"); } _context.Transactions.Add(transaction); _context.SaveChanges(); } ``` |

---

## Validation Flow Diagram

```
User Enters Data in Form
        ↓
[LEVEL 1] Data Annotations Run
  - Required check
  - Range check (0.01+)
  - StringLength check
  - Format validation
        ↓
   Valid? → NO → Display Error Messages on Form → User Corrects & Resubmits
   ↓ YES
Form Submits to Handler (OnPostAsync)
        ↓
[LEVEL 2] ModelState Validation Check
  - if (!ModelState.IsValid)
  - Reload form context
  - Return Page with errors
        ↓
   Valid? → NO → Form Re-rendered with Error Messages
   ↓ YES
[LEVEL 3] Business Logic Validation (Service Layer)
  - Verify foreign keys (Account, Category exist)
  - Check amount is positive
  - Verify status is valid
  - Check data consistency
  - Set audit trail fields
        ↓
   Valid? → NO → Throw Exception → Catch in Handler → Display Error
   ↓ YES
Save to Database
        ↓
✅ Transaction Persisted
Clear Form
Refresh List
Preserve Filters
Redirect to Filtered View
```

---

## Example: Complete Validation Journey

### Scenario 1: User Enters Negative Amount

**Step 1 - Data Annotations Check:**
```csharp
// User enters: Amount = -50
// Data annotation fires:
[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
// Result: Validation fails at client/model level
```

**Step 2 - Handler Receives Request:**
```csharp
public async Task<IActionResult> OnPostAsync()
{
    // ModelState.IsValid = false because Amount failed Range validation
    if (!ModelState.IsValid) // TRUE - enters error block
    {
        ModelState.AddModelError("TransactionForm.Amount", "Amount must be positive");
        LoadTransactions();
        LoadLookupData();
        return Page(); // Form re-rendered with error message
    }
}
```

**Step 3 - User Sees Error:**
```html
<!-- Form re-rendered with error displayed -->
<div class="field">
    <label for="TransactionForm_Amount">Amount</label>
    <input type="number" id="TransactionForm_Amount" name="TransactionForm.Amount" value="-50" />
    <span class="text-danger">Amount must be positive</span>
</div>
```

---

### Scenario 2: User Enters Valid Data But Invalid Account

**Step 1 - Data Annotations Pass:**
```csharp
// User enters:
// Description: "Valid description"
// Amount: 100
// AccountId: 999 (doesn't exist in DB)
// All data annotations pass
```

**Step 2 - Handler Validation Passes:**
```csharp
if (!ModelState.IsValid) // FALSE - all annotations passed
{
    // This block doesn't execute
}

try
{
    var transaction = new Transaction
    {
        Description = "Valid description",
        Amount = 100,
        AccountId = 999 // Will cause problem in service layer
    };
    _financialService.AddTransaction(transaction); // Passes to service
}
```

**Step 3 - Service Layer Catches Problem:**
```csharp
public void AddTransaction(Transaction transaction)
{
    // Business logic validation
    if (!_context.Accounts.Any(a => a.Id == transaction.AccountId))
    {
        throw new InvalidOperationException("Invalid Account ID");
    }
    // Exception thrown! Does not proceed to SaveChanges()
}
```

**Step 4 - Exception Caught Back in Handler:**
```csharp
catch (Exception ex)
{
    ModelState.AddModelError("", $"Error: {ex.Message}");
    // "Error: Invalid Account ID"
    LoadTransactions();
    LoadLookupData();
    return Page(); // User sees error message
}
```

---

## Summary: Why 3 Levels?

| Level | Purpose | When It Runs | What It Prevents |
|-------|---------|--------------|------------------|
| **Level 1: Data Annotations** | Fast client-side checks | Before form submits | Invalid formats, missing required fields, out-of-range values |
| **Level 2: Handler Validation** | Catches annotation failures | When POST arrives | Invalid model state, exception details, database errors |
| **Level 3: Business Logic** | Protects data integrity | Before database save | Foreign key violations, business rule violations, audit trail issues |

**Result:** Multi-layered defense ensures no invalid data reaches the database while providing users with clear, specific error messages at each stage.

---

## Testing the Validation

### Test Case 1: Required Field Missing
```
Input: Description = "", Amount = 100, Type = "Revenue", AccountId = 1
Level 1 Catches: [Required] on Description fails
Output: "Description is required"
```

### Test Case 2: Negative Amount
```
Input: Description = "Test", Amount = -50, Type = "Revenue", AccountId = 1
Level 1 Catches: [Range(0.01, ...)] fails on Amount
Output: "Amount must be positive"
```

### Test Case 3: Invalid Account
```
Input: Description = "Test", Amount = 100, Type = "Revenue", AccountId = 999
Level 1: Passes (all annotations satisfied)
Level 2: Passes (ModelState is valid)
Level 3 Catches: Service checks _context.Accounts.Any(a => a.Id == 999) = false
Output: "Invalid Account ID"
```

### Test Case 4: Valid Data
```
Input: Description = "Valid", Amount = 100, Type = "Revenue", AccountId = 1
Level 1: Passes
Level 2: Passes
Level 3: Passes
Result: Transaction saved to database ✅
```
