document.addEventListener("DOMContentLoaded", () => {
  renderPurchaseReport();
  document.getElementById("exportCsvBtn").addEventListener("click", exportSalesCSV);
});

async function renderPurchaseReport() {
  const container = document.getElementById("reportContainer");

  try {
    const report = await apiGet("customers/1/total-purchases");

    if (!report.length) {
      container.innerHTML = "<p>No purchase data found.</p>";
      return;
    }

    container.innerHTML = `
      <h4>Total Purchases by Customer</h4>
      <table class="table table-bordered table-sm">
        <thead>
          <tr>
            <th>Customer</th>
            <th>Total Purchase</th>
          </tr>
        </thead>
        <tbody>
          ${report.map(r => `
            <tr>
              <td>${r.customerId}</td>
              <td>$${r.totalPurchase.toFixed(2)}</td>
            </tr>
          `).join("")}
        </tbody>
      </table>
    `;
  } catch {
    showError("Failed to load report");
  }
}

async function exportSalesCSV() {
  try {
    const sales = await apiGet("sales");
    if (!sales.length) return showError("No sales data to export");

    let csv = "Customer,Product,Quantity,Total Price,Sale Date\n";
    sales.forEach(s => {
      csv += `"${s.customerName}","${s.productName}",${s.quantity},${s.totalPrice},"${new Date(s.saleDate).toLocaleDateString()}"\n`;
    });

    const blob = new Blob([csv], { type: "text/csv" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = `sales_report_${new Date().toISOString().slice(0,10)}.csv`;
    a.click();
    URL.revokeObjectURL(url);
  } catch {
    showError("Failed to export CSV");
  }
}
