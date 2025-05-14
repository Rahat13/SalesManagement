document.addEventListener("DOMContentLoaded", () => {
  renderSaleForm();
  loadSales();
});

// === Render Sale Form ===
async function renderSaleForm() {
  const container = document.getElementById("salesFormContainer");

  const customers = await apiGet("customers");
  const products = await apiGet("products");

  container.innerHTML = `
    <h4>Record a Sale</h4>
    <form id="saleForm">
      <div class="mb-2">
        <label>Customer:</label>
        <select id="saleCustomerId" class="form-control" required>
          <option value="">Select customer</option>
          ${customers.map(c => `<option value="${c.id}">${c.name}</option>`).join("")}
        </select>
      </div>

      <div class="mb-2">
        <label>Product:</label>
        <select id="saleProductId" class="form-control" required>
          <option value="">Select product</option>
          ${products.map(p => `<option value="${p.id}" data-price="${p.price}">${p.name} - $${p.price}</option>`).join("")}
        </select>
      </div>

      <div class="mb-2">
        <label>Quantity:</label>
        <input type="number" id="saleQuantity" class="form-control" min="1" value="1" required>
      </div>

      <div class="mb-2">
        <label>Total Price:</label>
        <input type="text" id="saleTotalPrice" class="form-control" readonly>
      </div>

      <button class="btn btn-success">Submit Sale</button>
    </form>
    <hr>
  `;

  document.getElementById("saleProductId").addEventListener("change", updateSaleTotal);
  document.getElementById("saleQuantity").addEventListener("input", updateSaleTotal);

  function updateSaleTotal() {
    const productSelect = document.getElementById("saleProductId");
    const quantity = parseInt(document.getElementById("saleQuantity").value || "0");
    const price = parseFloat(productSelect.options[productSelect.selectedIndex]?.dataset?.price || "0");
    const total = price * quantity;
    document.getElementById("saleTotalPrice").value = total.toFixed(2);
  }

  document.getElementById("saleForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const customerId = document.getElementById("saleCustomerId").value;
    const productId = document.getElementById("saleProductId").value;
    const quantity = parseInt(document.getElementById("saleQuantity").value);
    const totalPrice = parseFloat(document.getElementById("saleTotalPrice").value);

    const sale = { customerId, productId, quantity, totalPrice };
    try {
      await apiPost("sales", sale);
      showSuccess("Sale recorded");
      document.getElementById("saleForm").reset();
      loadSales();
    } catch {
      showError("Failed to save sale");
    }
  });
}

// === Load Sales + Filter ===
async function loadSales() {
  const container = document.getElementById("salesListContainer");

  const customers = await apiGet("customers");

  container.innerHTML = `
    <h4>Sales List</h4>
    <div class="row mb-3">
      <div class="col-md-4">
        <label>Customer</label>
        <select id="filterCustomer" class="form-control">
          <option value="">All Customers</option>
          ${customers.map(c => `<option value="${c.id}">${c.name}</option>`).join("")}
        </select>
      </div>
      <div class="col-md-3">
        <label>From Date</label>
        <input type="date" id="filterFromDate" class="form-control">
      </div>
      <div class="col-md-3">
        <label>To Date</label>
        <input type="date" id="filterToDate" class="form-control">
      </div>
      <div class="col-md-2 d-flex align-items-end">
        <button class="btn btn-primary w-100" onclick="applySalesFilter()">Apply Filter</button>
      </div>
    </div>
    <div id="salesTableContainer">Loading sales...</div>
  `;

  applySalesFilter(); // load initially
}

async function applySalesFilter() {
  const customerId = document.getElementById("filterCustomer").value;
  const fromDate = document.getElementById("filterFromDate").value;
  const toDate = document.getElementById("filterToDate").value;

  let query = [];
  if (customerId) query.push(`customerId=${customerId}`);
  if (fromDate) query.push(`fromDate=${fromDate}`);
  if (toDate) query.push(`toDate=${toDate}`);
  const queryStr = query.length ? "?" + query.join("&") : "";

  try {
    const sales = await apiGet("sales" + queryStr);
    renderSalesTable(sales);
  } catch {
    showError("Failed to load sales");
  }
}

function renderSalesTable(sales) {
  const container = document.getElementById("salesTableContainer");

  if (!sales.length) {
    container.innerHTML = "<p>No sales found.</p>";
    return;
  }

  container.innerHTML = `
    <table class="table table-bordered table-sm">
      <thead>
        <tr>
          <th>Customer</th>
          <th>Product</th>
          <th>Quantity</th>
          <th>Total Price</th>
          <th>Date</th>
        </tr>
      </thead>
      <tbody>
        ${sales
          .map(s => `
            <tr>
              <td>${s.customerName}</td>
              <td>${s.productName}</td>
              <td>${s.quantity}</td>
              <td>$${s.totalPrice.toFixed(2)}</td>
              <td>${new Date(s.saleDate).toLocaleDateString()}</td>
            </tr>
          `)
          .join("")}
      </tbody>
    </table>
  `;
}
