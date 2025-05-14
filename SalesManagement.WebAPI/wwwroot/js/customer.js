document.addEventListener("DOMContentLoaded", () => {
  renderCustomerForm();
  loadCustomers();
});

function renderCustomerForm(customer = null) {
  const container = document.getElementById("customerFormContainer");
  container.innerHTML = `
    <h4>${customer ? "Edit" : "Add"} Customer</h4>
    <form id="customerForm">
      <input type="hidden" id="customerId" value="${customer ? customer.id : ""}">
      <div class="mb-2">
        <input type="text" id="name" class="form-control" placeholder="Name" required value="${customer ? customer.name : ""}">
      </div>
      <div class="mb-2">
        <input type="email" id="email" class="form-control" placeholder="Email" required value="${customer ? customer.email : ""}">
      </div>
      <div class="mb-2">
        <input type="text" id="phone" class="form-control" placeholder="Phone" required value="${customer ? customer.phone : ""}">
      </div>
      <button class="btn btn-primary">${customer ? "Update" : "Add"}</button>
    </form>
    <hr>
  `;

  document.getElementById("customerForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const id = document.getElementById("customerId").value;
    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const phone = document.getElementById("phone").value.trim();

    const data = { name, email, phone };
    try {
      if (id) {
        await apiPut(`customers/${id}`, data);
        showSuccess("Customer updated");
      } else {
        await apiPost("customers", data);
        showSuccess("Customer added");
      }
      renderCustomerForm(); // reset form
      loadCustomers();
    } catch (err) {
      showError("Failed to save customer");
    }
  });
}

async function loadCustomers() {
  const container = document.getElementById("customerListContainer");
  container.innerHTML = `<p>Loading customers...</p>`;
  try {
    const customers = await apiGet("customers");
    if (!customers.length) {
      container.innerHTML = `<p>No customers found.</p>`;
      return;
    }

    container.innerHTML = `
      <h4>Customer List</h4>
      <table class="table table-bordered table-sm">
        <thead><tr><th>Name</th><th>Email</th><th>Phone</th><th>Actions</th></tr></thead>
        <tbody>
          ${customers
            .map(
              (c) => `
              <tr>
                <td>${c.name}</td>
                <td>${c.email}</td>
                <td>${c.phone}</td>
                <td>
                  <button class="btn btn-sm btn-warning" onclick="editCustomer(${c.id})">Edit</button>
                  <button class="btn btn-sm btn-danger" onclick="deleteCustomer(${c.id})">Delete</button>
                </td>
              </tr>
            `
            )
            .join("")}
        </tbody>
      </table>
    `;
  } catch (err) {
    showError("Could not load customers");
  }
}

async function editCustomer(id) {
  const customer = await apiGet(`customers/${id}`);
  renderCustomerForm(customer);
}

async function deleteCustomer(id) {
  if (!confirm("Are you sure you want to delete this customer?")) return;
  try {
    await apiDelete(`customers/${id}`);
    showSuccess("Customer deleted");
    loadCustomers();
  } catch {
    showError("Failed to delete customer");
  }
}
