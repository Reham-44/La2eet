// ===== بيانات منصة لقيت =====

const ITEMS = [
  {
    id: 1,
    title: "محفظة جلدية بنية",
    description: "محفظة جلدية بنية اللون، بها بطاقات هوية وبعض النقود. وجدتها في مترو المرج.",
    category: "محافظ",
    status: "found",
    city: "القاهرة",
    location: "محطة مترو المرج",
    date: "2025-04-20",
    image: "https://images.unsplash.com/photo-1627123424574-724758594e93?w=400&q=80",
    contact: "01012345678",
    postedBy: "أحمد محمد",
    verified: true,
    verificationQuestions: [
      { question: "كم عدد البطاقات داخل المحفظة؟", answer: "3" },
      { question: "ما لون البطانة الداخلية؟", answer: "أسود" }
    ]
  },
  {
    id: 2,
    title: "هاتف iPhone 14 أزرق",
    description: "هاتف آيفون 14 لون أزرق فاتح، به كفر شفاف. فُقد في منطقة المعادي.",
    category: "هواتف",
    status: "lost",
    city: "القاهرة",
    location: "المعادي - شارع 9",
    date: "2025-04-18",
    image: "https://images.unsplash.com/photo-1556656793-08538906a9f8?w=400&q=80",
    contact: "01198765432",
    postedBy: "منى علي",
    verified: false,
    verificationQuestions: []
  },
  {
    id: 3,
    title: "مفاتيح سيارة كيا",
    description: "مجموعة مفاتيح فيها مفتاح سيارة كيا ومفتاح بيت. وجدتها في كافيه الزمالك.",
    category: "مفاتيح",
    status: "found",
    city: "القاهرة",
    location: "الزمالك - كافيه على النيل",
    date: "2025-04-19",
    image: "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400&q=80",
    contact: "01234567890",
    postedBy: "كريم حسن",
    verified: true,
    verificationQuestions: [
      { question: "كم عدد المفاتيح في المجموعة؟", answer: "4" }
    ]
  },
  {
    id: 4,
    title: "حقيبة ظهر مدرسية زرقاء",
    description: "شنطة مدرسة زرقاء فيها كتب ودفاتر اسم الطالب عليها. ضاعت في الإسكندرية.",
    category: "حقائب",
    status: "lost",
    city: "الإسكندرية",
    location: "ستانلي - بجوار المدرسة",
    date: "2025-04-17",
    image: "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400&q=80",
    contact: "03112233445",
    postedBy: "سارة إبراهيم",
    verified: false,
    verificationQuestions: []
  },
  {
    id: 5,
    title: "نظارة طبية ذهبية",
    description: "نظارة طبية إطار ذهبي رفيع. وجدتها في عيادة في مدينة نصر.",
    category: "إكسسوارات",
    status: "found",
    city: "القاهرة",
    location: "مدينة نصر - عيادة الدكتور",
    date: "2025-04-16",
    image: "https://images.unsplash.com/photo-1574258495973-f010dfbb5371?w=400&q=80",
    contact: "01567891234",
    postedBy: "هبة يوسف",
    verified: true,
    verificationQuestions: [
      { question: "ما ماركة النظارة؟", answer: "rayban" }
    ]
  },
  {
    id: 6,
    title: "ساعة كاسيو سوداء",
    description: "ساعة كاسيو G-Shock سوداء. فقدتها في جيم الجيزة.",
    category: "ساعات",
    status: "lost",
    city: "الجيزة",
    location: "الجيزة - نادي سبورتنج",
    date: "2025-04-15",
    image: "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=400&q=80",
    contact: "01023456789",
    postedBy: "محمود عمر",
    verified: false,
    verificationQuestions: []
  },
  {
    id: 7,
    title: "قطة بيضاء صغيرة",
    description: "قطة بيضاء صغيرة عمرها تقريباً سنة، لها طوق وردي. ضاعت من المنطقة.",
    category: "حيوانات",
    status: "lost",
    city: "القاهرة",
    location: "مصر الجديدة - شارع النزهة",
    date: "2025-04-14",
    image: "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?w=400&q=80",
    contact: "01187654321",
    postedBy: "ريم أحمد",
    verified: false,
    verificationQuestions: []
  },
  {
    id: 8,
    title: "لاب توب Dell أسود",
    description: "لاب توب Dell XPS اسود، عليه ملصقات جامعة. وجدته في مقهى داون تاون.",
    category: "إلكترونيات",
    status: "found",
    city: "القاهرة",
    location: "وسط البلد - مقهى",
    date: "2025-04-13",
    image: "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400&q=80",
    contact: "01345678901",
    postedBy: "عمر خالد",
    verified: true,
    verificationQuestions: [
      { question: "ما هو الجامعة المكتوبة على الملصق؟", answer: "القاهرة" },
      { question: "ما لون الملصق الكبير؟", answer: "أحمر" }
    ]
  },
  {
    id: 9,
    title: "بطاقة هوية وطنية",
    description: "بطاقة رقم قومي باسم محمد أحمد، وجدتها على رصيف المحطة.",
    category: "وثائق",
    status: "found",
    city: "الجيزة",
    location: "محطة قطار الجيزة",
    date: "2025-04-12",
    image: "https://images.unsplash.com/photo-1555421689-491a97ff2040?w=400&q=80",
    contact: "01456789012",
    postedBy: "ياسمين فاروق",
    verified: false,
    verificationQuestions: [
      { question: "ما هو تاريخ الميلاد على البطاقة؟", answer: "1990" }
    ]
  },
  {
    id: 10,
    title: "طوق كلب أحمر",
    description: "طوق كلب أحمر فيه اسم الكلب وجدته في الحديقة.",
    category: "حيوانات",
    status: "found",
    city: "الإسكندرية",
    location: "حديقة الشلالات",
    date: "2025-04-11",
    image: "https://images.unsplash.com/photo-1601758174486-03e00843a3a0?w=400&q=80",
    contact: "03298765432",
    postedBy: "لينا حسام",
    verified: false,
    verificationQuestions: []
  },
  {
    id: 11,
    title: "سماعات AirPods Pro",
    description: "سماعات ابل ايربودز بروا في علبتها البيضاء. وجدتها في التاكسي.",
    category: "إلكترونيات",
    status: "found",
    city: "القاهرة",
    location: "مصر الجديدة - تاكسي",
    date: "2025-04-10",
    image: "https://images.unsplash.com/photo-1606220945770-b5b6c2c55bf1?w=400&q=80",
    contact: "01678901234",
    postedBy: "تامر رشاد",
    verified: true,
    verificationQuestions: [
      { question: "هل في خدش على العلبة؟", answer: "نعم" }
    ]
  },
  {
    id: 12,
    title: "حقيبة يد نسائية وردية",
    description: "شنطة يد وردية صغيرة فيها مستلزمات شخصية. ضاعت من السيتي ستارز.",
    category: "حقائب",
    status: "lost",
    city: "القاهرة",
    location: "سيتي ستارز - دور الملابس",
    date: "2025-04-09",
    image: "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?w=400&q=80",
    contact: "01789012345",
    postedBy: "دينا عبدالله",
    verified: false,
    verificationQuestions: []
  }
];

const CATEGORIES = ["الكل", "محافظ", "هواتف", "مفاتيح", "حقائب", "إكسسوارات", "ساعات", "حيوانات", "إلكترونيات", "وثائق", "أخرى"];
const CITIES = ["الكل", "القاهرة", "الجيزة", "الإسكندرية", "المنصورة", "أسوان", "الأقصر", "طنطا", "الفيوم"];

const STATS = {
  totalItems: 1240,
  returned: 820,
  users: 3500,
  cities: 18
};

// Utility functions
function formatDate(dateStr) {
  const d = new Date(dateStr);
  return d.toLocaleDateString("ar-EG", { year: "numeric", month: "long", day: "numeric" });
}

function getStatusBadge(status) {
  return status === "found"
    ? '<span class="badge bg-success"><i class="fas fa-check-circle me-1"></i>تم الإيجاد</span>'
    : '<span class="badge bg-danger"><i class="fas fa-exclamation-circle me-1"></i>مفقود</span>';
}

function getCategoryIcon(cat) {
  const icons = {
    "محافظ": "fa-wallet", "هواتف": "fa-mobile-alt", "مفاتيح": "fa-key",
    "حقائب": "fa-shopping-bag", "إكسسوارات": "fa-glasses", "ساعات": "fa-clock",
    "حيوانات": "fa-paw", "إلكترونيات": "fa-laptop", "وثائق": "fa-id-card", "أخرى": "fa-box"
  };
  return icons[cat] || "fa-box";
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
