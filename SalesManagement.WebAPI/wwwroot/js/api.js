const API_BASE = "https://localhost:44355/api"; // change if needed

async function apiGet(url) {
  showLoader();
  try {
    const res = await fetch(`${API_BASE}/${url}`);
    return await res.json();
  } catch (err) {
    showError("Error fetching data");
    throw err;
  } finally {
    hideLoader();
  }
}

async function apiPost(url, data) {
  showLoader();
  try {
    const res = await fetch(`${API_BASE}/${url}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    return await res.json();
  } catch (err) {
    showError("Error posting data");
    throw err;
  } finally {
    hideLoader();
  }
}

async function apiPut(url, data) {
  showLoader();
  try {
    const res = await fetch(`${API_BASE}/${url}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    return res.ok;
  } finally {
    hideLoader();
  }
}

async function apiDelete(url) {
  showLoader();
  try {
    const res = await fetch(`${API_BASE}/${url}`, { method: "DELETE" });
    return res.ok;
  } finally {
    hideLoader();
  }
}
