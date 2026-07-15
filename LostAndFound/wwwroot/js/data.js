//function formatDate(dateStr) {
//  const d = new Date(dateStr);
//  return d.toLocaleDateString("ar-EG", { year: "numeric", month: "long", day: "numeric" });
//}

function getStatusBadge(status) {
  return status === "found"
    ? '<span class="badge bg-success"><i class="fas fa-check-circle me-1"></i>تم الإيجاد</span>'
    : '<span class="badge bg-danger"><i class="fas fa-exclamation-circle me-1"></i>مفقود</span>';
}



function escapeHtml(value) {
  return String(value ?? '').replace(/[&<>"']/g, ch => ({
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&#039;'
  }[ch]));
}

function safeUrl(value, fallback = '') {
  const url = String(value || '').trim();
  if (/^(https?:|data:image\/)/i.test(url)) return url;
  return fallback;
}

function safePhone(value) {
  return String(value || '').replace(/[^\d+]/g, '');
}

function saveToStorage(key, data) {
  localStorage.setItem(key, JSON.stringify(data));
}

function getFromStorage(key) {
  try { return JSON.parse(localStorage.getItem(key)) || []; }
  catch { return []; }
}

// Merge stored items with default
function getAllItems() {
  const stored = getFromStorage("laqeet_items");
  return [...ITEMS, ...stored];
}
