function showLoader() {
  if (!document.getElementById("loader")) {
    const div = document.createElement("div");
    div.id = "loader";
    div.className = "text-center my-2";
    div.innerHTML = `<div class="spinner-border" role="status"><span class="visually-hidden">Loading...</span></div>`;
    document.body.appendChild(div);
  }
}

function hideLoader() {
  const loader = document.getElementById("loader");
  if (loader) loader.remove();
}

function showError(msg) {
  alert(msg); // Improve with Bootstrap alert later
}

function showSuccess(msg) {
  alert(msg); // Improve with Bootstrap alert later
}
